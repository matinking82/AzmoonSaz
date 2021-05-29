using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.ViewModels.Tests
{
    public record TestsClassViewModel
    {
        public int ClassId { get; set; }
        
        public string ClassName { get; set; }

        public bool IsMyClass { get; set; }

        public List<TestsListViewModel> Tests { get; set; }
    }

    public record TestsListViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public int QuestionsCount { get; set; }
    }
}
