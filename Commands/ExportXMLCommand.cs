using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task_2.Commands
{
    public class ExportXMLCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            
        }

        public override bool CanExecute(object? parameter) => false;
    }
}
