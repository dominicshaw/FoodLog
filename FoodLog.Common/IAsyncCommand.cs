using System;
using System.Threading.Tasks;

namespace FoodLog.Common
{
    public interface IAsyncCommand
    {
        Task ExecuteAsync(object parameter);
        bool CanExecute(object parameter);
        event EventHandler CanExecuteChanged;
        event EventHandler ShouldExecute;

    }
}
