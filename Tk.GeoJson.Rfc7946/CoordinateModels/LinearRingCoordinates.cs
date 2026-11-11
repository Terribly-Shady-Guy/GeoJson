using System.Collections;
using System.Runtime.CompilerServices;

namespace Tk.GeoJson.Rfc7946.CoordinateModels;

[CollectionBuilder(typeof(LinearRingCoordinates), nameof(Create))]
public sealed class LinearRingCoordinates : IReadOnlyList<Position>, ICoordinates
{
    private readonly IList<Position> _items;

    public LinearRingCoordinates(IList<Position> positions)
    {
        if (positions.Count < 4 || positions[0] != positions[^1])
        {
            throw new ArgumentException("A valid LinearRing requires at least 4 positions and the first and last positions must be identical.");
        }

        _items = positions;
        Dimensions = positions.All(position => position.Dimensions == 3) ? 3 : 2;
    }

    public Position this[int index]
    {
        get
        {
            if (index < 0 || index >= _items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The provided index is out of bounds.");
            }

            return _items[index];
        }
    }

    public int Count => _items.Count;
    public int Dimensions { get; }

    public IEnumerator<Position> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static LinearRingCoordinates Create(params ReadOnlySpan<Position> items)
    {
        return new LinearRingCoordinates(items.ToArray());
    }
}
