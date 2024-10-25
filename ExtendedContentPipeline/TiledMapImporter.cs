using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System.IO;

using TImport = System.String;

namespace ExtendedContentPipeline
{
    [ContentImporter(".tmj", DisplayName = "Tiled Map Importer")]
    public class TiledMapImporter : ContentImporter<TiledMap>
    {
        public override TiledMap Import(string filename, ContentImporterContext context)
        {
            // Load the .tmj file
            string json = File.ReadAllText(filename);
            // Parse the JSON into a TiledMap object
            TiledMap map = ParseTiledMap(json);
            return map;
        }

        private TiledMap ParseTiledMap(string json)
        {
            // Implement your JSON parsing logic here
            // Use a library like Newtonsoft.Json or System.Text.Json
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TiledMap>(json);
        }
    }
}
