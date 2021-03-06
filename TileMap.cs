﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RobotRampage
{
    static class TileMap
    {
        //pg. 189
        #region Declarations
        public const int TileWidth = 32;
        public const int TileHeight = 32;
        public const int MapWidth = 50;
        public const int MapHeight = 50;

        public const int FloorTileStart = 0;
        public const int FloorTileEnd = 3;
        public const int WallTileStart = 4;
        public const int WallTileEnd = 7;

        static private Texture2D texture;
        static private List<Rectangle> tiles = new List<Rectangle>();

        static private int[,] mapSquares = new int[MapWidth, MapHeight];

        static private Random rand = new Random();
        #endregion

        //pg. 190
        #region Initialization
        static public void Initialize(Texture2D tileTexture)
        {
            texture = tileTexture;
            tiles.Clear();
            tiles.Add(new Rectangle(0, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(32, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(64, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(96, 0, TileWidth, TileHeight));
            tiles.Add(new Rectangle(0, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(32, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(64, 32, TileWidth, TileHeight));
            tiles.Add(new Rectangle(96, 32, TileWidth, TileHeight));

            for (int x = 0; x < MapWidth; x++)
                for (int y = 0; y < MapHeight; y++)
                {
                    mapSquares[x, y] = FloorTileStart;
                }
        }
        #endregion 

        //pg. 191
        #region Information about Map Squares

        static public int GetSquareByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }
        static public int GetSquareByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }
        static public Vector2 GetSquareAtPixel(Vector2 pixelLocation)
        {
            return new Vector2(
                GetSquareByPixelX((int)pixelLocation.X),
                GetSquareByPixelY((int)pixelLocation.Y));
        }

        static Vector2 GetSquareCenter(int squareX, int squareY)
        {
            return new Vector2(
                (squareX * TileWidth) + (TileWidth / 2),
                (squareY * TileHeight) + (TileHeight / 2));
        }

        static public Vector2 GetSquareCenter(Vector2 square)
        {
            return GetSquareCenter(
                (int)square.X,
                (int)square.Y);
        }

        static public Rectangle SquareWorldRectangle(int x, int y)
        {
            return new Rectangle(
                x * TileWidth,
                y * TileHeight,
                TileWidth,
                TileHeight);
        }

        static public Rectangle SquareWorldRectangle(Vector2 square)
        {
            return SquareWorldRectangle(
                (int)square.X,
                (int)square.Y);
        }

        static public Rectangle SquareScreenRectangle(int x, int y)
        {
            return Camera.Transform(SquareWorldRectangle(x, y));
        }

        static public Rectangle SquareScreenRectangle(Vector2 square)
        {
            return SquareScreenRectangle((int)square.X, (int)square.Y);
        }
        #endregion

        //pg. 194
        #region Information about Map Tiles

        static public int GetTileAtSquare(int tileX, int tileY)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                return mapSquares[tileX, tileY];
            }
            else
            {
                return -1;
            }
        }
        static public void SetTileAtSquare(int tileX, int tileY, int tile)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                mapSquares[tileX, tileY] = tile;
            }
        }

        static public int GetTileAtPixel(int pixelX, int pixelY)
        {
            return GetTileAtSquare(
                GetSquareByPixelX(pixelX),
                GetSquareByPixelX(pixelY));
        }
        static public int GetTileAtPixel(Vector2 pixelLocation)
        {
            return GetTileAtPixel(
                (int)pixelLocation.X,
                (int)pixelLocation.Y);
        }

        static public bool IsWallTile(int tileX, int tileY)
        {
            int tileIndex = GetTileAtSquare(tileX, tileY);

            if (tileIndex == -1)
            {
                return false;
            }

            return tileIndex >= WallTileStart;
        }
        static public bool IsWallTile(Vector2 square)
        {
            return IsWallTile((int)square.X, (int)square.Y);
        }

        static public bool IsWallTileByPixel(Vector2 pixelLocation)
        {
            return IsWallTile(
                GetSquareByPixelX((int)pixelLocation.X),
                GetSquareByPixelY((int)pixelLocation.Y));
        }
        #endregion
    }
}
