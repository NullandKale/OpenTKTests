using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKTest1
{
    public class text
    {
        public Tile[] tiles;
        public Texture2D tex;

        public text(letter[] letters, TextureAtlas tAtlas)
        {
            tiles = new Tile[letters.Length];

            for(int i = 0; i < letters.Length; i++)
            {
                tiles[i] = new Tile();
                tiles[i].TexID = (int)letters[i];
                tiles[i].tAtlas = tAtlas;
            }

            tex = ContentPipe.TextureFrom1DTileMap(tiles);
        }
    }
}
