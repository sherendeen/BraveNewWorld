using System;
using System.Windows.Forms;

namespace BraveNewWorld
{
    /// <summary>
    /// This class used to have quite a lot of stuff in it
    /// but I think Jonah put it back into FormGame.
    /// - Seth
    /// 
    /// </summary>
    public class EventHelper
    {

        /// <summary>
        /// Finds the control that is in focus, my dude.
        /// </summary>
        /// <param name="control">The control that is in focus</param>
        /// <returns></returns>
        public Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }


        /// <summary>
        /// Updates CRIME. ALL CRIME. IT IS UPDATED HERE
        /// </summary>
        /// <returns>Returns a headline in the form of a string</returns>
        


    }
}
