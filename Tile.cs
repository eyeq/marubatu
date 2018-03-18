using System;
using System.ComponentModel;

namespace marubatu
{
    public class Tile
    {
        public enum State
        {
            None,
            Maru,
            Batu,
            Sankaku,
            Shikaku,
        }
        
        public State Status;

        public Tile() : this(State.None)
        {
        }

        public Tile(State status)
        {
            Status = status;
        }
        
        public override string ToString()
        {
            return Status.GetLabel();
        }
    }
    
    public static class TileExtensions {

        public static string GetLabel(this Tile.State state)
        {
            var text = new[]{"", "○", "×", "△", "□", "●", "+", "▲", "■"};
            return text[(int)state];
        }
    }
}