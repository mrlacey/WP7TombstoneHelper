using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    public static class PhoneApplicationPageExtensions
    {
        private static int pivotItemIndex = -1;

        private static bool inAListbox = false;

        private const string PAGE_CONTAINS_PIVOT = "PageContainsPivot";

        private static Pivot pivotToRestoreTo;

        // This to be called in OnNavigatingFrom
        public static void SaveState(this PhoneApplicationPage page, NavigatingCancelEventArgs e, params Type[] typesToSave)
        {
            page.SaveState(e, int.MaxValue, typesToSave);
        }

        // This to be called in OnNavigatingFrom
        public static void SaveState(this PhoneApplicationPage page, NavigatingCancelEventArgs e, int maxItems, params Type[] typesToSave)
        {
            // Improve performance by not saving state if it is about to be destroyed
            if (e.NavigationMode != NavigationMode.Back)
            {
                page.SaveState(maxItems, typesToSave);
            }
        }

        // This to be called in OnNavigatedFrom
        public static void SaveState(this PhoneApplicationPage page, params Type[] typesToSave)
        {
            page.SaveState(int.MaxValue, typesToSave);
        }

        // This to be called in OnNavigatedFrom
        public static void SaveState(this PhoneApplicationPage page, int maxItems, params Type[] typesToSave)
        {
            page.State.Clear();

            var tombstoners = AllSupportedTombstoners(typesToSave);

            var counter = 0;

            if (tombstoners.ContainsKey(typeof(PhoneApplicationPage)))
            {
                tombstoners[typeof(PhoneApplicationPage)].Save(page, pivotItemIndex, page);
            }

            foreach (var toSave in page.NamedChildrenOfTypes(tombstoners.Keys.ToArray()))
            {
                if (toSave is FakeFrameworkElementActingAsPivotIndicator)
                {
                    // There should never be multiple pivots on a page, but just in case
                    if (!page.State.ContainsKey(PAGE_CONTAINS_PIVOT))
                    {
                        page.State.Add(PAGE_CONTAINS_PIVOT, true);
                    }
                }
                else if (toSave is FakeFrameworkElementActingAsPivotItemStartIndicator)
                {
                    pivotItemIndex = (toSave as FakeFrameworkElementActingAsPivotItemStartIndicator).PivotItemIndex;
                }
                else if (toSave is FakeFrameworkElementActingAsPivotItemEndIndicator)
                {
                    pivotItemIndex = -1;
                }
                else if (toSave is FakeFrameworkElementActingAsListBoxStartIndicator)
                {
                    inAListbox = true;
                }
                else if (toSave is FakeFrameworkElementActingAsListBoxEndIndicator)
                {
                    inAListbox = false;
                }
                else
                {
                    // Don't include the ScrollViewer inside a ListBox - this is handled by the ListBox
                    // Both are included if no types are specified
                    if (inAListbox && (toSave is ScrollViewer))
                    {
                        continue;
                    }

                    tombstoners[toSave.GetType()].Save(toSave, pivotItemIndex, page);

                    if (++counter == maxItems)
                    {
                        break;
                    }
                }
            }
        }

        public static void RestoreState(this PhoneApplicationPage page)
        {
            if (page.State.Keys.Count > 0)
            {
                if (page.State.ContainsKey(PAGE_CONTAINS_PIVOT))
                {
                    pivotToRestoreTo = page.ChildrenOfType<Pivot>().First();

                    page.State.Remove(PAGE_CONTAINS_PIVOT);

                    // If we get here `pivot` should never be null but let's keep the compiler happy
                    if (pivotToRestoreTo != null)
                    {
                        // If the page contains a Pivot control we'll only be able to walk the full visual tree
                        // (so we can restore anything in a PivotItem) once the Pivot has Loaded
                        pivotToRestoreTo.Loaded += (s, e) => page.RestoreState();
                    }
                }
                else
                {
                    var typesToRestore = new List<Type>();

                    var pivotItemIndexes = new Dictionary<string, string>();

                    var restorersAndDetails = new Dictionary<string, KeyValuePair<object, ICanTombstone>>();

                    var allRestorers = AllTombstoneRestorers();

                    foreach (var key in page.State.Keys.Where(k => k.Contains("^")))
                    {
                        var keyParts = key.Split('^');

                        var restorerType = keyParts[0];

                        if (allRestorers.ContainsKey(restorerType))
                        {
                            if (restorerType == "PhoneApplicationPage")
                            {
                                allRestorers[restorerType].Value.Restore(page, keyParts[1]);
                            }
                            else
                            {
                                if (!typesToRestore.Contains(allRestorers[restorerType].Key))
                                {
                                    typesToRestore.Add(allRestorers[restorerType].Key);
                                }

                                pivotItemIndexes.Add(keyParts[1], keyParts[2]);

                                restorersAndDetails.Add(keyParts[1],
                                                        new KeyValuePair<object, ICanTombstone>(page.State[key],
                                                                                                allRestorers[restorerType].Value));
                            }
                        }
                    }

                    foreach (var toRestore in page.NamedChildrenOfTypes(typesToRestore))
                    {
                        if (restorersAndDetails.Keys.Contains(toRestore.Name))
                        {
                            FrameworkElement restore = toRestore;
                            Action op = () => restorersAndDetails[restore.Name].Value.Restore(restore, restorersAndDetails[restore.Name].Key);

                            if (pivotItemIndexes[toRestore.Name] != "-1")
                            {
                                (pivotToRestoreTo.Items[int.Parse(pivotItemIndexes[toRestore.Name])] as PivotItem).Loaded += (s, e) => op.Invoke();
                                pivotItemIndexes.Remove(toRestore.Name);
                            }
                            else
                            {
                                op.Invoke();
                            }
                        }
                    }

                    pivotItemReloaders = new Dictionary<int, EventHandler>(pivotToRestoreTo == null ? 0 : pivotToRestoreTo.Items.Count);

                    foreach (var itemIndex in pivotItemIndexes)
                    {
                        if ((itemIndex.Value != "-1") && !string.IsNullOrEmpty(itemIndex.Key))
                        {
                            var p2r = pivotToRestoreTo.Items[int.Parse(itemIndex.Value)] as PivotItem;

                            var key = int.Parse(itemIndex.Value);

                            if (!pivotItemReloaders.ContainsKey(key))
                            {
                                KeyValuePair<string, string> index = itemIndex;

                                EventHandler reloader = (s, e) =>
                                {
                                    bool unloaded = false;

                                    foreach (var toRestore in p2r.NamedChildrenOfTypes(typesToRestore))
                                    {
                                        if (restorersAndDetails.Keys.Contains(toRestore.Name))
                                        {
                                            if (!unloaded)
                                            {
                                                p2r.LayoutUpdated -= pivotItemReloaders[int.Parse(index.Value)];
                                                unloaded = true;
                                            }

                                            restorersAndDetails[toRestore.Name].Value.Restore(toRestore, restorersAndDetails[toRestore.Name].Key);
                                        }
                                    }
                                };

                                pivotItemReloaders.Add(key, reloader);

                                p2r.LayoutUpdated += pivotItemReloaders[int.Parse(itemIndex.Value)];
                            }
                        }
                    }
                }
            }
        }

        private static Dictionary<int, EventHandler> pivotItemReloaders;

        internal static IEnumerable<FrameworkElement> NamedChildrenOfTypes(this FrameworkElement root, IEnumerable<Type> types)
        {
            if (types.Contains(root.GetType()) && !string.IsNullOrEmpty(root.Name))
            {
                yield return root;
            }

            if (root is Pivot)
            {
                yield return new FakeFrameworkElementActingAsPivotIndicator();
            }

            if (root is ScrollViewer)
            {
                var svc = (root as ScrollViewer).Content;

                if ((svc != null) && (svc is FrameworkElement))
                {
                    var svcfe = svc as FrameworkElement;

                    var count = VisualTreeHelper.GetChildrenCount(svcfe);

                    for (var idx = 0; idx < count; idx++)
                    {
                        foreach (var child in NamedChildrenOfTypes(VisualTreeHelper.GetChild(svcfe, idx) as FrameworkElement, types))
                        {
                            yield return child;
                        }
                    }
                }
            }
            else
            {
                if (root is PivotItem)
                {
                    yield return new FakeFrameworkElementActingAsPivotItemStartIndicator(((Pivot)(root.Parent)).Items.IndexOf(root));
                }

                if (root is ListBox)
                {
                    yield return new FakeFrameworkElementActingAsListBoxStartIndicator();
                }

                var count = VisualTreeHelper.GetChildrenCount(root);

                for (var idx = 0; idx < count; idx++)
                {
                    foreach (var child in NamedChildrenOfTypes(VisualTreeHelper.GetChild(root, idx) as FrameworkElement, types))
                    {
                        yield return child;
                    }
                }

                if (root is PivotItem)
                {
                    yield return new FakeFrameworkElementActingAsPivotItemEndIndicator();
                }

                if (root is ListBox)
                {
                    yield return new FakeFrameworkElementActingAsListBoxEndIndicator();
                }
            }
        }

        internal static IEnumerable<T> ChildrenOfType<T>(this DependencyObject root) where T : FrameworkElement
        {
            if ((root is T) && !string.IsNullOrEmpty((root as FrameworkElement).Name))
            {
                yield return root as T;
            }

            if (root != null)
            {
                if (root is ScrollViewer)
                {
                    var svc = (root as ScrollViewer).Content;

                    if ((svc != null) && (svc is FrameworkElement))
                    {
                        var svcfe = svc as FrameworkElement;

                        var count = VisualTreeHelper.GetChildrenCount(svcfe);

                        for (var idx = 0; idx < count; idx++)
                        {
                            foreach (var child in ChildrenOfType<T>(VisualTreeHelper.GetChild(svcfe, idx)))
                            {
                                yield return child;
                            }
                        }
                    }
                }
                else
                {
                    var count = VisualTreeHelper.GetChildrenCount(root);

                    for (var idx = 0; idx < count; idx++)
                    {
                        foreach (var child in ChildrenOfType<T>(VisualTreeHelper.GetChild(root, idx)))
                        {
                            yield return child;
                        }
                    }
                }
            }
        }

        private static Dictionary<string, KeyValuePair<Type, ICanTombstone>> AllTombstoneRestorers()
        {
            return new Dictionary<string, KeyValuePair<Type, ICanTombstone>>
            {
                { "TextBox", new KeyValuePair<Type, ICanTombstone>(typeof(TextBox), new TextBoxTombstoner()) },
                { "CheckBox", new KeyValuePair<Type, ICanTombstone>(typeof(CheckBox), new CheckBoxTombstoner()) },
                { "PasswordBox", new KeyValuePair<Type, ICanTombstone>(typeof(PasswordBox), new PasswordBoxTombstoner()) },
                { "Slider", new KeyValuePair<Type, ICanTombstone>(typeof(Slider), new SliderTombstoner()) },
                { "RadioButton", new KeyValuePair<Type, ICanTombstone>(typeof(RadioButton), new RadioButtonTombstoner()) },
                { "ScrollViewer", new KeyValuePair<Type, ICanTombstone>(typeof(ScrollViewer), new ScrollViewerTombstoner()) },
                { "ListBox", new KeyValuePair<Type, ICanTombstone>(typeof(ListBox), new ListBoxTombstoner()) },
                { "ToggleButton", new KeyValuePair<Type, ICanTombstone>(typeof(ToggleButton), new ToggleButtonTombstoner()) },
                { "PhoneApplicationPage", new KeyValuePair<Type, ICanTombstone>(typeof(PhoneApplicationPage), new PhoneApplicationPageTombstoner()) },
                { "Pivot", new KeyValuePair<Type, ICanTombstone>(typeof(Pivot), new PivotTombstoner()) }
            };
        }

        private static Dictionary<Type, ICanTombstone> AllSupportedTombstoners(params Type[] filteredTypesToSave)
        {
            var result = new Dictionary<Type, ICanTombstone>();

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(PhoneApplicationPage)))
            {
                result.Add(typeof(PhoneApplicationPage), new PhoneApplicationPageTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(TextBox)))
            {
                result.Add(typeof(TextBox), new TextBoxTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(CheckBox)))
            {
                result.Add(typeof(CheckBox), new CheckBoxTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(PasswordBox)))
            {
                result.Add(typeof(PasswordBox), new PasswordBoxTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(Slider)))
            {
                result.Add(typeof(Slider), new SliderTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(RadioButton)))
            {
                result.Add(typeof(RadioButton), new RadioButtonTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(ScrollViewer)))
            {
                result.Add(typeof(ScrollViewer), new ScrollViewerTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(ListBox)))
            {
                result.Add(typeof(ListBox), new ListBoxTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(ToggleButton)))
            {
                result.Add(typeof(ToggleButton), new ToggleButtonTombstoner());
            }

            if ((filteredTypesToSave.Count() == 0) || filteredTypesToSave.Contains(typeof(Pivot)))
            {
                result.Add(typeof(Pivot), new PivotTombstoner());
            }

            return result;
        }
    }
}
