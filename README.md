# WP7TombstoneHelper

! Tombstoning support just got REALLY simple!

This library adds extension methods to PhoneApplicationPage so you don't have to worry about maintaining the state of a page in your app if it gets tombstoned.

*It just takes 2 lines of code!*

```
protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
{
    this.SaveState(e);  // <- first line
}

protected override void OnNavigatedTo(NavigationEventArgs e)
{
    this.RestoreState();  // <- second line
}
```

If you add the above to your code then the _contents, checked state and scroll positions_ of *TextBoxes*, *PasswordBoxes*, *CheckBoxes*, *RadioButtons*, *Sliders*, *ListBoxes* and *ScrollViewers* will be preserved.

If you want to add support for other types this can be done as the code was designed to be _*easily extensible*_.

The code is _*optimised for performance*_ but if you don't use all of those types on your page you can get even better performance by just specifying the types of the objects you wish to save the state of:


`    this.SaveState(typeof(ScrollViewer));`

or 
`    this.SaveState(typeof(TextBox), typeof(PasswordBox), typeof(CheckBox));`
or 
`    this.SaveState<TextBox, PasswordBox, CheckBox>();`



!! *Yes. It's reallly that simple.*
You just need to make sure you're given a name to the object you wish to save the state of.

{code:xml}
    <TextBox Name="thisHasANameAndSoWillBeSaved" />
    <TextBox /><!--This TextBox does not have a name so it's contents won't be automatically saved and restored -->
{code:xml}

_Please note that there are much better ways of using it. See the [Documentation] page for more details._

All feedback, comments, questions, etc. greatly appreciated.
