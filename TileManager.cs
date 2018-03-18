using System;

namespace marubatu
{
    public class TileManager
    {
        public readonly int Width;
        public readonly int Height;
        public readonly int Player;

        public Tile.State Current;
        
        private readonly Tile[][] Tiles;
        
        public TileManager(int width, int height, int player)
        {
            Width = width;
            Height = height;
            Player = player;
            
            Current = Tile.State.Maru;
            
            Tiles = new Tile[height][];
            for (var i = 0; i < Height; i++)
            {
                Tiles[i] = new Tile[Width];
                for (var j = 0; j < Width; j++)
                {
                    Tiles[i][j] = new Tile();
                }
            }
        }
        
        private Tile GetTile(int x, int y)
        {
            if (x < 0 || Width <= x || y < 0 || Height <= y)
            {
                return new Tile();
            }
            return Tiles[y][x];
        }

        public Tile.State GetTileState(int x, int y)
        {
            return GetTile(x, y).Status;
        }

        public bool Put(int x, int y)
        {
            var tile = GetTile(x, y);

            if (tile.Status != Tile.State.None)
            {
                return false;
            }

            tile.Status = Current;
            return true;
        }

        public Tile.State Next()
        {
            if ((int) Current == Player)
            {
                Current = Tile.State.Maru;
            }
            else
            {
                Current ++;
            }
            return Current;
        }

        public int GetEmpty()
        {
            var count = 0;
            
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (GetTileState(j, i) == Tile.State.None)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        
        public int GetMaxLine(Tile.State state)
        {
            var maxCount = 0;
            var count = 0;
            
            for (var i = 0; i < Height; i++)
            {
                count = 0;
                for (var j = 0; j < Width; j++)
                {
                    if (GetTileState(j, i) == state)
                    {
                        count++;
                    }
                    else
                    {
                        maxCount = Math.Max(count, maxCount);
                        count = 0;
                    }
                }
                maxCount = Math.Max(count, maxCount);
            }
            
            for (var j = 0; j < Width; j++)
            {
                count = 0;
                for (var i = 0; i < Height; i++)
                {
                    if (GetTileState(j, i) == state)
                    {
                        count++;
                    }
                    else
                    {
                        maxCount = Math.Max(count, maxCount);
                        count = 0;
                    }
                }
                maxCount = Math.Max(count, maxCount);
            }
            
            for (var i = -Height; i < Width + Height; i++)
            {
                count = 0;
                for (var j = -Width; j < Width + Height; j++)
                {
                    if (GetTileState(j, i + j) == state)
                    {
                        count++;
                    }
                    else
                    {
                        maxCount = Math.Max(count, maxCount);
                        count = 0;
                    }
                }
                maxCount = Math.Max(count, maxCount);
                
                count = 0;
                for (var j = -Width; j < Width + Height; j++)
                {
                    if (GetTileState(j, i - j) == state)
                    {
                        count++;
                    }
                    else
                    {
                        maxCount = Math.Max(count, maxCount);
                        count = 0;
                    }
                }
                maxCount = Math.Max(count, maxCount);
            }
            
            return maxCount;
        }
    }
}