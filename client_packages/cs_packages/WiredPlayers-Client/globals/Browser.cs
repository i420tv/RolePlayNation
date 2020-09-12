using RAGE;
using RAGE.Ui;
using System.Linq;

namespace WiredPlayers_Client.globals
{
    class Browser : Events.Script
    {
        private static object[] parameters = null;
        public static HtmlWindow customBrowser = null;

        public Browser()
        {
            Events.Add("createBrowser", CreateBrowserEvent);
            Events.Add("executeFunction", ExecuteFunctionEvent);
            Events.Add("destroyBrowser", DestroyBrowserEvent);
            Events.OnBrowserCreated += OnBrowserCreatedEvent;
        }

        public static void CreateBrowserEvent(object[] args)
        {
            if (customBrowser == null)
            {
                // Get the URL from the parameters
                string url = args[0].ToString();

                // Save the rest of the parameters
                parameters = args.Skip(1).ToArray();

                // Create the browser
                customBrowser = new HtmlWindow(url);
            }
        }

        public static void ExecuteFunctionEvent(object[] args)
        {
            // Check for the parameters
            string input = string.Empty;

            // Split the function and arguments
            string function = args[0].ToString();
            object[] arguments = args.Skip(1).ToArray();

            foreach (object arg in arguments)
            {
                // Append all the arguments
                input += input.Length > 0 ? (", '" + arg.ToString() + "'") : ("'" + arg.ToString() + "'");
            }

            // Call the function with the parameters
            customBrowser.ExecuteJs(function + "(" + input + ");");
        }

        public static void DestroyBrowserEvent(object[] args)
        {
            // Disable the cursor
            Cursor.Visible = false;

            // Destroy the browser
            customBrowser.Destroy();
            customBrowser = null;
        }

        public static void OnBrowserCreatedEvent(HtmlWindow window)
        {
            if (window.Id == customBrowser.Id)
            {
                // Enable the cursor
                Cursor.Visible = true;

                if(parameters.Length > 0)
                {
                    // Call the function passed as parameter
                    ExecuteFunctionEvent(parameters);
                }
            }
        }
    }
}