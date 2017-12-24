using System;

namespace Logic.Base
{
    public enum CellType
    {
        Room,
        Pillar,
        Gate,
    }
    public class SceneCell
    {
        private SceneRoot root;
        private CellType type;
        private AABB boundBox;
        private SceneCell[] adjacents = new SceneCell[8];
        //格子索引，从左到右
        private int x;
        //从下到上
        private int y;
        public bool Wallkable { get; set; }
        public CellType Type { get { return type; } }
        public AABB BoundBox { get { return boundBox; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }

        public SceneCell this[int index]
        {
            get
            {
                return adjacents[index];
            }
        }

        public SceneCell this[DirectionType type]
        {
            get
            {
                return adjacents[(int)type];
            }
        }

        public SceneCell(SceneRoot root, CellType type, AABB boundBox, int x, int y)
        {
            this.root = root;
            this.type = type;
            this.boundBox = boundBox;
            this.x = x;
            this.y = y;
        }

        public void Init()
        {
            foreach (DirectionType type in Enum.GetValues(typeof(DirectionType)))
            {
                Vector v = Vector.Direction[(int)type];
                int posX = (int)v.X + x;
                int posY = (int)v.Y + y;
                adjacents[(int)type] = root.GetSceneCell(posX, posY);
            }
        }
    }
}
