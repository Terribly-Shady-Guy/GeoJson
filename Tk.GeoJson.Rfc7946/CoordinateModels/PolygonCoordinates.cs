using System.Collections;
using System.Collections.ObjectModel;

namespace Tk.GeoJson.Rfc7946.CoordinateModels;

public sealed class PolygonCoordinates : Collection<LinearRingCoordinates>, ICoordinates
{
    public PolygonCoordinates() : base()
    {
        Dimensions = 0;    
    }

    public PolygonCoordinates(int numDimensions) : base()
    {
        Dimensions = numDimensions;
    }

    public PolygonCoordinates(IList<LinearRingCoordinates> items) : base(items)
    {
        Dimensions = 0;
    }

    public PolygonCoordinates(int numDimensions, IList<LinearRingCoordinates> items) : base(items)
    {
        Dimensions = numDimensions;
    }

    public int Dimensions { get; private set; }
    public LinearRingCoordinates ExteriorRing => Items[0];
    public InteriorRingsCollection Holes => new(Items);

    protected override void InsertItem(int index, LinearRingCoordinates item)
    {
        if (Dimensions == 0)
        {
            Dimensions = item.Dimensions;
        }

        ThrowArgumentExceptionOnMismatchedDimensions(item);
        base.InsertItem(index, item);
    }

    protected override void SetItem(int index, LinearRingCoordinates item)
    {
        if (Dimensions == 0)
        {
            Dimensions = item.Dimensions;
        }

        ThrowArgumentExceptionOnMismatchedDimensions(item);
        base.SetItem(index, item);
    }

    private void ThrowArgumentExceptionOnMismatchedDimensions(LinearRingCoordinates item)
    {
        if (item.Dimensions != Dimensions)
        {
            throw new ArgumentException($"This Polygon only allows {Dimensions} dimensions, got {item.Dimensions} instead.", nameof(item));
        }
    }
}

public readonly struct InteriorRingsCollection : IReadOnlyList<LinearRingCoordinates>
{
    private readonly IList<LinearRingCoordinates> _positions;

    internal InteriorRingsCollection(IList<LinearRingCoordinates> positions)
    {
        _positions = positions;
    }

    public LinearRingCoordinates this[int index]
    {
        get
        {
            if (index < 0 || index >= _positions.Count - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The provided index is out of range.");
            }

            return _positions[index + 1];
        }
    }

    public int Count => _positions.Count - 1;

    public IEnumerator<LinearRingCoordinates> GetEnumerator()
    {
        for (int i = 1; i < _positions.Count - 1; i++)
        {
            yield return _positions[i + 1];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}