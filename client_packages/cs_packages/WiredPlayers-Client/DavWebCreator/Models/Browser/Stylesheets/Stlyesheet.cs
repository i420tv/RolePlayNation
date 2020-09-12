namespace DavWebCreator.Client.ClientModels.Browser.Stylesheets
{
   public  class Stylesheet
    {
        public string Attribut { get; set; }
        public string Value { get; set; }

        public Stylesheet(string attribut, string value)
        {
            this.Attribut = attribut;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{Attribut}:{Value};";
        }
    }
}
