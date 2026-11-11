using System.Collections;
using System.Runtime.CompilerServices;

namespace Tk.GeoJson.Rfc7946.CoordinateModels;

[CollectionBuilder(typeof(PolygonCoordinates), nameof(Create))]
public sealed class PolygonCoordinates : IReadOnlyList<LinearRingCoordinates>, ICoordinates
{
    private readonly IList<LinearRingCoordinates> _items;

    public PolygonCoordinates(IList<LinearRingCoordinates> positions)
    {
        _items = positions;
        Dimensions = positions.All(line => line.Dimensions == 3) ? 3 : 2;
    }

    public LinearRingCoordinates this[int index]
    {
        get
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The provided index is out of range.");
            }

            return _items[index];
        }
    }

    public int Dimensions { get; }
    public LinearRingCoordinates ExteriorRing => _items[0];
    public InteriorRingsCollection Holes => new(_items);

    public int Count => _items.Count;

    public IEnumerator<LinearRingCoordinates> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static PolygonCoordinates Create(params ReadOnlySpan<LinearRingCoordinates> positions)
    {
        return new PolygonCoordinates(positions.ToArray());
    }
}

public readonly ref struct InteriorRingsCollection : IReadOnlyList<LinearRingCoordinates>
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
        for (int i = 1; i < _positions.Count; i++)
        {
            yield return _positions[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}