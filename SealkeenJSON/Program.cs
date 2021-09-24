//CREATED BY SILKIN IVAN FOR EPAM TRAINING COURSES ONLY
//THIS PRODUCT IS NOT COVERED BY ANY LICENSE AND GIVEN YOU
//"AS IS". DESPITE THAT, YOU ARE NOT ALLOWED TO USE THIS
//SIGNATURE IF YOU MODIFIED ANY PART OF THE PROGRAM WITHOUT
//MY PERSONAL AGREEMENT SO BE CAREFUL NOT TO SIGN YOUR
//MODIFICATION USING MY SIGNATURE, IT WOULD BE UNFAIR!
//                  2016 EPAM TRAINING.
    
using System;
using System.Windows.Forms;

namespace EPAM.CSCourse2016.SilkinIvan.SealkeenJSON
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThreadAttribute]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmTest());
        }
    }
}
