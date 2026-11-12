using System.Collections.ObjectModel;

namespace Tk.GeoJson.Rfc7946.CoordinateModels;

public sealed class LineStringCoordinates : Collection<Position>, ICoordinates
{
    public LineStringCoordinates() : base()
    {
        Dimensions = 0;
    }

    public LineStringCoordinates(int numDimensions) : base()
    {
        Dimensions = numDimensions;  
    }

    public LineStringCoordinates(IList<Position> items) : base(items)
    {
        Dimensions = 0;
    }

    public LineStringCoordinates(int numDimensions, IList<Position> positions) : base(positions)
    {
        Dimensions = numDimensions;
    }

    public int Dimensions { get; private set; }
    internal bool IsValidLineString => Count < 2;

    protected override void InsertItem(int index, Position item)
    {
        if (Dimensions == 0)
        {
            Dimensions = item.Dimensions;
        }

        ThrowArgumentExceptionIfMismatchedDimensions(item);
        base.InsertItem(index, item);
    }

    protected override void SetItem(int index, Position item)
    {
        if (Dimensions == 0)
        {
            Dimensions = item.Dimensions;
        }

        ThrowArgumentExceptionIfMismatchedDimensions(item);
        base.SetItem(index, item);
    }

    private void ThrowArgumentExceptionIfMismatchedDimensions(Position item)
    {
        if (item.Dimensions != Dimensions)
        {
            throw new ArgumentException($"Instance only allows {Dimensions} dimensions, got {item.Dimensions} dimensions.", nameof(item));
        }
    }
}