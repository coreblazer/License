using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.Interfaces
{
    interface INav
    {
        Task GoBackToPreviousPageAsync(Page page);
    }
}
