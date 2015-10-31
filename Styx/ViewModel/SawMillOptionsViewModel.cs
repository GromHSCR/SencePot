using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Styx.GromHSCR.MvvmBase.ViewModels;

namespace Styx.ViewModel
{
    public class SawMillOptionsViewModel : ViewModelBase
    {
        public SawMillOptionsViewModel()
        {
            _isSawPdf = true;
            _isSawRtf = true;
        }

        private bool _isDesolateOriginal;
        public bool IsDesolateOriginal
        {
            get { return _isDesolateOriginal; }
            set
            {
                if (value == _isDesolateOriginal) return;
                RaisePropertyChanging("IsDesolateOriginal");
                _isDesolateOriginal = value;
                RaisePropertyChanged("IsDesolateOriginal");
            }
        }

        private bool _isSawRtf;
        public bool IsSawRtf
        {
            get { return _isSawRtf; }
            set
            {
                if (value == _isSawRtf) return;
                RaisePropertyChanging("IsSawRtf");
                _isSawRtf = value;
                RaisePropertyChanged("IsSawRtf");
            }
        }

        private bool _isSawPdf;
        public bool IsSawPdf
        {
            get { return _isSawPdf; }
            set
            {
                if (value == _isSawPdf) return;
                RaisePropertyChanging("IsSawPdf");
                _isSawPdf = value;
                RaisePropertyChanged("IsSawPdf");
            }
        }

        private string _inputFolder;
        public string InputFolder
        {
            get { return _inputFolder; }
            set
            {
                if (value == _inputFolder) return;
                RaisePropertyChanging("InputFolder");
                _inputFolder = value;
                RaisePropertyChanged("InputFolder");
            }
        }

        private string _outputFolder;
        public string OutputFolder
        {
            get { return _outputFolder; }
            set
            {
                if (value == _outputFolder) return;
                RaisePropertyChanging("OutputFolder");
                _outputFolder = value;
                RaisePropertyChanged("OutputFolder");
            }
        }

    }
}
