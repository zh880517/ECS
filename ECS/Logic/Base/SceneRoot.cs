using System.Collections.Generic;
namespace Logic.Base
{
    public class SceneRoot
    {
        private AABB sceneArea;
        private float roomWidth;//房间宽度
        private float roomHigh;//房间高度
        private float pillarWidth;//柱子宽度
        private float pillarHigh;//柱子高度
        private int roomXNum;
        private int roomYNum;

        private Dictionary<int, SceneCell> cells = new Dictionary<int, SceneCell>();

        public SceneRoot(int roomXNum, int roomYNum, float roomWidth, float roomHigh, float pillarWidth, float pillarHigh)
        {
            float maxX = roomXNum * roomWidth + (roomXNum - 1) * pillarWidth;
            float maxY = roomYNum * roomHigh + (roomYNum - 1) * pillarHigh;
            sceneArea = new AABB(Vector.Zero, maxX, maxY);
            this.roomHigh = roomHigh;
            this.roomWidth = roomWidth;
            this.pillarHigh = pillarHigh;
            this.pillarWidth = pillarWidth;
            Init();
        }
        
        private void Init()
        {
            if (cells.Count > 0)
                return;
            for (int i=0; i< roomXNum; ++i)
            {
                for (int j = 0; j<roomYNum; ++j)
                {
                    int x = i * 2;
                    int y = j * 2;
                    int key = MakeKey(x, y);
                    Vector vPos = new Vector(i * (roomWidth + pillarWidth), j * (roomHigh + pillarHigh));
                    SceneCell cell = new SceneCell(this, CellType.Room, new AABB(vPos, roomWidth, roomHigh), x, y);
                    cells.Add(key, cell);
                }
            }
            for (int i = 0; i < roomXNum-1; ++i)
            {
                for (int j = 0; j < roomYNum-1; ++j)
                {
                    //水平方向的门
                    {
                        int x = i * 2 + 1;
                        int y = j * 2;
                        int key = MakeKey(x, y);
                        Vector vPos = new Vector(i * (roomWidth + pillarWidth), j * (roomHigh + pillarHigh) + roomHigh);
                        SceneCell cell = new SceneCell(this, CellType.Gate, new AABB(vPos, roomWidth, pillarHigh), x, y);
                        cells.Add(key, cell);
                    }
                    //垂直方向的门
                    {
                        int x = i * 2;
                        int y = j * 2 + 1;
                        int key = MakeKey(x, y);
                        Vector vPos = new Vector(i * (roomWidth + pillarWidth) + roomWidth, j * (roomHigh + pillarHigh));
                        SceneCell cell = new SceneCell(this, CellType.Pillar, new AABB(vPos, pillarWidth, roomHigh), x, y);
                        cells.Add(key, cell);
                    }
                }
            }
            for (int i = 0; i < roomXNum - 1; ++i)
            {
                for (int j = 0; j < roomYNum - 1; ++j)
                {
                    int x = i * 2 + 1;
                    int y = j * 2 + 1;
                    int key = MakeKey(x, y);
                    Vector vPos = new Vector(i * (roomWidth + pillarWidth) + roomWidth, j * (roomHigh + pillarHigh) + roomHigh);
                    SceneCell cell = new SceneCell(this, CellType.Room, new AABB(vPos, pillarWidth, pillarHigh), x, y);
                    cells.Add(key, cell);
                }
            }
            foreach (var cell in cells)
            {
                cell.Value.Init();
            }
        }

        public SceneCell GetSceneCell(int x, int y)
        {
            int key = MakeKey(x, y);
            SceneCell cell = null;
            cells.TryGetValue(key, out cell);
            return cell;
        }

        public void GetTouchingCell(AABB aABB, List<SceneCell> list)
        {

        }

        private int MakeKey(int x, int y)
        {
            return x | y << 16;
        }
    }
}
