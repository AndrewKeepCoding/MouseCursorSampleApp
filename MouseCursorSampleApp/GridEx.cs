using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Immutable;

namespace MouseCursorSampleApp;

public class GridEx : Grid
{
    public static readonly DependencyProperty EnableCursorOverrideProperty =
        DependencyProperty.Register(
            nameof(EnableCursorOverride),
            typeof(bool),
            typeof(GridEx),
            new PropertyMetadata(default, (d, _) => (d as GridEx)?.UpdateCursor()));

    public static readonly DependencyProperty InputSystemCursorShapeProperty =
        DependencyProperty.Register(
            nameof(InputSystemCursorShape),
            typeof(InputSystemCursorShape),
            typeof(GridEx),
            new PropertyMetadata(default, (d, _) => (d as GridEx)?.UpdateCursor()));

    static GridEx()
    {
        CursorOptions = ImmutableArray.Create(Enum.GetValues<InputSystemCursorShape>());
    }

    public GridEx()
    {
        Background = new SolidColorBrush(Colors.Transparent);
    }

    public static ImmutableArray<InputSystemCursorShape> CursorOptions { get; }

    public InputSystemCursorShape InputSystemCursorShape
    {
        get => (InputSystemCursorShape)GetValue(InputSystemCursorShapeProperty);
        set => SetValue(InputSystemCursorShapeProperty, value);
    }

    public bool EnableCursorOverride
    {
        get => (bool)GetValue(EnableCursorOverrideProperty);
        set => SetValue(EnableCursorOverrideProperty, value);
    }

    private void UpdateCursor()
    {
        ProtectedCursor =
            EnableCursorOverride is true &&
            InputSystemCursor.Create(InputSystemCursorShape) is InputCursor inputCursor
                ? inputCursor
                : null;
    }
}
