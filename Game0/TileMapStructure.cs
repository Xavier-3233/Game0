/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game0
{
    public class TileMapStructure
    {
        public TileMap LoadTileMap(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<TileMap>(json);
        }


        public class TileLayer
        {
            public List<int> Data { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
            public string Name { get; set; }
        }

        public void DrawTileMap(TileMap tileMap, SpriteBatch spriteBatch)
        {
            foreach (var layer in tileMap.layers)
            {
                for (int y = 0; y < layer.Height; y++)
                {
                    for (int x = 0; x < layer.Width; x++)
                    {
                        int tileIndex = layer.Data[y * layer.Width + x];

                        if (tileIndex != 0) // Assuming 0 is no tile
                        {
                            Texture2D tileTexture = GetTextureForTile(tileIndex); // Implement this method
                            spriteBatch.Draw(tileTexture, new Vector2(x * tileWidth, y * tileHeight), Color.White);
                        }
                    }
                }
            }
        }
    }
}*/
