using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop.Models
{
    class WorkspaceModel
    {
        public BindableCollection<Document> Documents { get; set; } = new BindableCollection<Document> ();
        public int ArtifactId { get; set; }


        public class Document
        {
            public int ArtifactId { get; set; }
            public string Identifier { get; set; }
        }
    }
}
