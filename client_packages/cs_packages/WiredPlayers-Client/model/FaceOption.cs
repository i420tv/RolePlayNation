namespace WiredPlayers_Client.model
{
    class FaceOption
    {
        public string desc { get; set; }
        public int minValue { get; set; }
        public int maxValue { get; set; }

        public FaceOption() { }

        public FaceOption(string desc, int minValue, int maxValue)
        {
            this.desc = desc;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}
