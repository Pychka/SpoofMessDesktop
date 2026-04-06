using SpoofFileParser.FileMetadata;
using SpoofMess.Models;
using System.Windows;
using System.Windows.Controls;

namespace SpoofMess.ViewElements;

public class ImagePanel : Panel
{
    protected override Size MeasureOverride(Size availableSize)
    {
        double width = Math.Min(availableSize.Width, 300), 
            height = 0;
        int index = InternalChildren.Count % 3;
        List<UIElement> list = [.. InternalChildren.Cast<UIElement>()];
        if (index != 0)
            height += GetHeight2(list[..index], width);
        for (; index < InternalChildren.Count; index += 3)
            height += GetHeight2(list.Slice(index, 3), width);

        return new Size(width, height);
    }

    private static double GetHeight2(List<UIElement> childrens, double width)
    {
        double height = 1;
        foreach (UIElement element in childrens)
            if (element is FrameworkElement { DataContext: FileObject { Metadata: ImageMetadata metadata } })
                height += (double)metadata.Width / metadata.Height;

        return width / height;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        int count = InternalChildren.Count;
        if (count == 0)
            return finalSize;
        int firstItems = count % 3, 
            rowCount = count / 3, 
            childIndex = 0;

        Rect rect;
        List<UIElement> list = [.. InternalChildren.Cast<UIElement>()];
        double height = GetHeight2(list[..firstItems], finalSize.Width),
            itemWidth, 
            currentX = 0, 
            currentY = 0;
        for (int r = 0; r < firstItems; r++)
        {
            if (childIndex >= count)
                return finalSize;
            if (list[childIndex] is FrameworkElement { DataContext: FileObject { Metadata: ImageMetadata metadata } })
            {
                itemWidth = (double)metadata.Width / metadata.Height * height;
                rect = new(currentX, 0, itemWidth, height);
                currentX += itemWidth;
                InternalChildren[childIndex++].Arrange(rect);
            }
        }
        for (int r = 0; r < rowCount; r++)
        {
            currentX = 0;
            currentY += height;
            height = GetHeight2(list.Slice(firstItems + r * 3, 3), finalSize.Width);
            for (int c = 0; c < 3; c++)
            {
                if (childIndex >= count)
                    return finalSize;
                if (list[childIndex] is FrameworkElement { DataContext: FileObject { Metadata: ImageMetadata metadata } })
                {
                    itemWidth = (double)metadata.Width / metadata.Height * height;
                    rect = new(currentX, currentY, itemWidth, height);
                    currentX += itemWidth;
                    InternalChildren[childIndex++].Arrange(rect);
                }
            }
        }
        return finalSize;
    }
}
