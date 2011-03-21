using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;

namespace TombstoneHelper
{
    public static class PhoneApplicationPageExtensions
    {
        public static void SaveState(this PhoneApplicationPage page, params Type[] typesToSave)
        {
            page.State.Clear();

            var tombstoners = AllSupportedTombstoners(typesToSave);

            foreach (var toSave in page.NamedChildrenOfTypes(tombstoners.Keys.ToArray()))
            {
                tombstoners[toSave.GetType()].Save(toSave, page);
            }
        }

        public static void RestoreState(this PhoneApplicationPage page)
        {
            // TODO: change this so we only have to walk the tree once
            foreach (var key in page.State.Keys)
            {
                var restorerType = key.Split('^')[0];

                var restorers = AllTombstoneRestorers();

                if (restorers.ContainsKey(restorerType))
                {
                    restorers[restorerType].Restore(page, key);
                }
            }
        }

        internal static IEnumerable<FrameworkElement> NamedChildrenOfTypes(this FrameworkElement root, IEnumerable<Type> types)
        {
            if (types.Contains(root.GetType()) && !string.IsNullOrEmpty(root.Name))
            {
                yield return root;
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
                var count = VisualTreeHelper.GetChildrenCount(root);

                for (var idx = 0; idx < count; idx++)
                {
                    foreach (var child in NamedChildrenOfTypes(VisualTreeHelper.GetChild(root, idx) as FrameworkElement, types))
                    {
                        yield return child;
                    }
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

        private static Dictionary<string, ICanTombstone> AllTombstoneRestorers()
        {
            return new Dictionary<string, ICanTombstone>
                       {
                           { "TextBox", new TextBoxTombstoner() },
                           { "CheckBox", new CheckBoxTombstoner() },
                           { "PasswordBox", new PasswordBoxTombstoner() },
                           { "Slider", new SliderTombstoner() },
                           { "RadioButton", new RadioButtonTombstoner() },
                           { "ScrollViewer", new ScrollViewerTombstoner() },
                           { "ListBox", new ListBoxTombstoner() }
                       };
        }

        private static Dictionary<Type, ICanTombstone> AllSupportedTombstoners(params Type[] filteredTypesToSave)
        {
            var result = new Dictionary<Type, ICanTombstone>();

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

            return result;
        }
    }
}
