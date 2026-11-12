using System.Collections.ObjectModel;

namespace Tk.GeoJson.Rfc7946.CoordinateModels;

public sealed class LinearRingCoordinates : Collection<Position>, ICoordinates
{
    public LinearRingCoordinates() : base()
    {
        Dimensions = 0;
    }

    public LinearRingCoordinates(int numDimensions) : base()
    {
        Dimensions = numDimensions;
    }

    public LinearRingCoordinates(IList<Position> items) : base(items)
    {
        Dimensions = 0;
    }

    public LinearRingCoordinates(int numDimensions, IList<Position> items) : base(items)
    {
        Dimensions = numDimensions;
    }

    public int Dimensions { get; private set; }
    internal bool IsValidLinearRing => Count >= 4 && Items[0] == Items[^1];

    protected override void InsertItem(int index, Position item)
    {
        if (Dimensions == 0)
        {
            Dimensions = item.Dimensions;
        }

        ThrowArgumentExceptionOnMismatchedDimensions(item);
        base.InsertItem(index, item);
    }

    protected override void SetItem(int index, Position item)
    {
        if (Dimensions == 0)
        {
            Dimensions = item.Dimensions;
        }

        ThrowArgumentExceptionOnMismatchedDimensions(item);
        base.SetItem(index, item);
    }

    private void ThrowArgumentExceptionOnMismatchedDimensions(Position item)
    {
        if (item.Dimensions != Dimensions)
        {
            throw new ArgumentException($"The Linear ring only allows {Dimensions} dimensions, got {item.Dimensions} dimensions.", nameof(item));
        }
    }
}
