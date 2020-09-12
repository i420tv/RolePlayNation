namespace WiredPlayers.model
{
    public class UniformModel
    {
        public int type { get; set; }
        public int factionJob { get; set; }
        public int characterSex { get; set; }
        public int uniformSlot { get; set; }
        public int uniformDrawable { get; set; }
        public int uniformTexture { get; set; }

        public UniformModel(int type, int factionJob, int characterSex, int uniformSlot, int uniformDrawable, int uniformTexture)
        {
            this.type = type;
            this.factionJob = factionJob;
            this.characterSex = characterSex;
            this.uniformSlot = uniformSlot;
            this.uniformDrawable = uniformDrawable;
            this.uniformTexture = uniformTexture;
        }
    }
}
