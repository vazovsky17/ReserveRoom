using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReserveRoom.Models;
using ReserveRoom.Services;
using ReserveRoom.Stores;
using ReserveRoom.ViewModels;

namespace ReserveRoom.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService _navigateService;

        public NavigateCommand(NavigationService navigateService)
        {
            _navigateService = navigateService;
        }

        public override void Execute(object? parameter)
        {
            _navigateService.Navigate();
        }
    }
}
