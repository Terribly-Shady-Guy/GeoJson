using System.Collections;
using System.Runtime.CompilerServices;

namespace Tk.GeoJson.Rfc7946.CoordinateModels;

[CollectionBuilder(typeof(LineStringCoordinates), nameof(Create))]
public sealed class LineStringCoordinates : IReadOnlyList<Position>, ICoordinates
{
    private readonly IList<Position> _items;

    public LineStringCoordinates(IList<Position> positions)
    {
        if (positions.Count < 2)
        {
            throw new ArgumentException("A valid LineString requires at least 2 positions.");
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
                throw new ArgumentOutOfRangeException(nameof(index), "The provided index is out of range.");
            }

            return _items[index];
        }
    }

    public int Dimensions { get; }
    public int Count => _items.Count;

    public IEnumerator<Position> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static LineStringCoordinates Create(params ReadOnlySpan<Position> positions)
    {
        return new LineStringCoordinates(positions.ToArray());
    }
}
