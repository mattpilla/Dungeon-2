namespace Dungeon_2
{
    public class AnimationDefinition
    {
        private int[] _order;
        private float _rate;
        public int Index { get; set; } = 0;

        public AnimationDefinition(int[] order, int rate) {
            _order = order;
            _rate = rate;
        }

        public int[] order() {
            return _order;
        }

        public float rate() {
            return _rate;
        }
    }
}
