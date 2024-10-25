using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Input; // Ensure you have Newtonsoft.Json NuGet package installed

namespace Game0
{
    public class TileMap
    {

        private int clickCount = 0;
        private bool bossBattleInitiated = false;
        // Tile dimensions and map dimensions
        private int _tileWidth, _tileHeight, _mapWidth, _mapHeight;

        // Tileset texture and tile info
        private Texture2D [] _tilesetTexture = new Texture2D[5];
        private Rectangle[] _tiles;

        // The tile map data
        private int[] _map;

        /// <summary>
        /// The filename of the map file
        /// </summary>
        string _filename;

        public TileMap(string filename)
        {
            _filename = filename;
        }

        public void LoadContent(ContentManager content)
        {
            // Read the TMJ file
            string data = File.ReadAllText(Path.Join(content.RootDirectory, _filename));
            var tilemapData = JsonConvert.DeserializeObject<TilemapData>(data);

            // Gets the sp
            string tilesetSource;
           

            // Load tileset texture (ensure it's added to the Content Pipeline)
            for(int i = 0; i < 5; i++)
            {
                tilesetSource = Path.GetFileNameWithoutExtension(tilemapData.tilesets[i].source);
                _tilesetTexture[i] = content.Load<Texture2D>(tilesetSource);
            }
            

            // Set tile width and height
            _tileWidth = tilemapData.tilewidth;
            _tileHeight = tilemapData.tileheight;

            // Set map dimensions
            _mapWidth = tilemapData.width;
            _mapHeight = tilemapData.height;

            // Calculate the number of tiles in the tileset
            int tilesetColumns = _tilesetTexture[0].Width / _tileWidth;
            int tilesetRows = _tilesetTexture[0].Height / _tileHeight;
            _tiles = new Rectangle[tilesetColumns * tilesetRows];

            // Initialize tiles
            for (int y = 0; y < tilesetRows; y++)
            {
                for (int x = 0; x < tilesetColumns; x++)
                {
                    int index = y * tilesetColumns + x;
                    _tiles[index] = new Rectangle(
                        x * _tileWidth,
                        y * _tileHeight,
                        _tileWidth,
                        _tileHeight
                    );
                }
            }

            // Set the map data (assuming only one layer for simplicity)
            _map = tilemapData.layers[0].data;
        }

        /*public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    // Calculate the tile ID based on the current position in the map
                    int tileId = _map[y * _mapWidth + x]; // Assuming _data holds the tile IDs

                    // Skip empty tiles (ID 0)
                    if (tileId == 0)
                        continue;

                    // Convert tile ID to index for the _tiles array
                    int index = tileId - 1; // Convert to zero-based index

                    // Ensure the index is valid
                    if (index < 0 || index >= _tiles.Length)
                    {
                        Console.WriteLine($"Invalid tile index: {index} for tileId: {tileId}");
                        continue; // Skip drawing if the index is invalid
                    }

                    // Draw the tile using the calculated index
                    spriteBatch.Draw(
                        _tilesetTexture,
                        new Vector2(x * _tileWidth, y * _tileHeight),
                        _tiles[index],
                        Color.White
                    );
                }
            }
        }*/

        // This method assumes you have a method to draw a tile at a specific position.
        // Replace DrawTile with your actual drawing method.
        private void DrawTile(int tileIndex, int x, int y, GameTime gameTime, SpriteBatch spriteBatch)
        {

            //spriteBatch.Begin();
            spriteBatch.Draw(_tilesetTexture[4],
                        new Vector2(x, y),
            _tiles[0],
                        Color.White);
        }

        public void DrawTileGrid(GameTime gameTime, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            int numRows = 10;
            int numCols = 10;

            // Make sure to adjust this based on your actual tilemap layer and data structure
            int offsetX = (screenWidth - 320) / 2;
            int offsetY = (screenHeight - 320) / 2;


            MouseState mouseState = Mouse.GetState();
            bool isClick = mouseState.LeftButton == ButtonState.Pressed;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    // Calculate the index in the flat tile data array
                    int index = row * numCols + col;

                    // Check if the index is valid to avoid out-of-bounds errors
                    if (index < _map.Length)
                    {
                        int tileIndex = _map[index];
                        int tileX = col * _tileWidth + offsetX;
                        int tileY = row * _tileHeight + offsetY;

                        Rectangle tileBounds = new Rectangle(tileX, tileY, _tileWidth, _tileHeight);

                        if (isClick && tileBounds.Contains(mouseState.Position))
                        {
                            if (clickCount < 6 && tileIndex != 9)
                            {
                                // Replace the tile with a random tile (1, 2, 3, or revealed)
                                Random random = new Random();
                                _map[index] = random.Next(1, 4);  // Random between 1, 2, 3, or 4 (revealed)

                                // Increment the click count
                                clickCount++;

                                // On the 6th click, change the tile to 9
                                if (clickCount == 6)
                                {
                                    _map[index] = 9;
                                    bossBattleInitiated = true;
                                }
                            }
                        }
                        else
                        {
                            DrawTile(_map[index], tileX, tileY, gameTime, spriteBatch);
                        }

                       // DrawTile(_map[index], tileX, tileY, gameTime, spriteBatch);
                        

                        /*// Check if the tile is unrevealed
                        if (tileIndex == 5)
                        {
                            DrawTile(4, tileX, tileY, gameTime, spriteBatch);
                            // Optionally draw a hidden tile or skip
                            // e.g., DrawTile(hiddenTileIndex, col * tileWidth, row * tileHeight);
                        }
                        else
                        {
                            // Draw the actual tile
                            DrawTile(4, tileX, tileY, gameTime, spriteBatch);
                        }*/
                    }
                }
            }
        }


        public class TilemapData
        {
            public int tilewidth { get; set; }
            public int tileheight { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public List<Tileset> tilesets { get; set; }
            public List<Layer> layers { get; set; }
        }

        public class Tileset
        {
            public string source { get; set; }
        }

        public class Layer
        {
            public int[] data { get; set; }
        }
    }
}