#region Copyright Notice

// Distributed under the Code Project Open License 
// ****************************************************
// 
// Copyright ?2011. David C. Veeneman
// All rights reserved 
// 
// Redistribution and use on source and binary forms, with or without modification 
// are permitted provided that the following conditions are met:
// 
// ?Redistributions of source code must retain the above copyright notice, this 
// list of conditions and the following disclaimers. 
// 
// ?Redistributions on binary form must reproduce the above copyright notice, this 
// list of conditions and the following disclaimer in the documentation anclior other 
// materials provided with the distribution. 
// 
// ?Neither the name of David C. Veeneman nor Foresight Systems may be used to endorse 
// or promote products derived from this software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER 'AS IS' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL DAVID C VEENEMAN OR FORERSIGHT SYSTEMS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSITTUIE GOODS OR SERVICES: LOSS OF USE, DATA OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT. 

#endregion

using System;
using System.Threading;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class ProgressDialogViewModel : BindableBase
    {
        #region Fields

        // Property variables
        private int p_Progress;
        private string p_ProgressMessage;
        private int p_ProgressMax;

        //Member variables
        private string m_ProgressMessageTemplate;
        private string m_CancellationMessage;

        #endregion

        #region Constructor

        public ProgressDialogViewModel()
        {
            CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            Initialize();
        }

        #endregion

        public DelegateCommand<object> CancelCommand { get; private set; }

  
        public void Cancel(object parameter)
        {

            if (IsCancelled) return;

            if (TokenSource == null)
            {
                throw new ApplicationException("ProgressDialogViewModel.TokenSource property is null");
            }
            // Cancel all pending background tasks
            TokenSource.Cancel();
            IsShow = false;
        }

        public bool CanCancel(object parameter)
        {
            return true;
        }

        #region Admin Properties

        /// <summary>
        /// A cancellation token source for the background operations.
        /// </summary>
        internal CancellationTokenSource TokenSource { get; set; }

        /// <summary>
        /// Whether the operation in progress has been cancelled.
        /// </summary>
        /// <remarks> 
        /// The Cancel command is invoked by the Cancel button, and on the window
        /// close (in case the user clicks the close box to cancel. The Cancel 
        /// command sets this property and checks it to make sure that the command 
        /// isn't run twice when the user clicks the Cancel button (once for the 
        /// button-click, and once for the window-close.
        /// </remarks>
        public bool IsCancelled { get; set; }
        #endregion

        #region Data Properties

        /// <summary>
        /// The progress of an image processing job.
        /// </summary>
        /// <remarks>
        /// The setter for this property also sets the ProgressMessage property.
        /// </remarks>
        public int Progress
        {
            get { return p_Progress; }

            set
            {
               // base.RaisePropertyChangingEvent("Progress");
                p_Progress = value;
                OnPropertyChanged(() => Progress);
              //  base.RaisePropertyChangedEvent("Progress");
            }
        }

        /// <summary>
        /// The maximum progress value.
        /// </summary>
        /// <remarks>
        /// The 
        /// </remarks>
        public int ProgressMax
        {
            get { return p_ProgressMax; }

            set
            {
              //  base.RaisePropertyChangingEvent("ProgressMax");
                p_ProgressMax = value;
                OnPropertyChanged(() => ProgressMax);
               // base.RaisePropertyChangedEvent("ProgressMax");
            }
        }

        /// <summary>
        /// The status message to be displayed in the View.
        /// </summary>
        public string ProgressMessage
        {
            get { return p_ProgressMessage; }

            set
            {
              //  base.RaisePropertyChangingEvent("ProgressMessage");
                p_ProgressMessage = value;
                OnPropertyChanged(() => ProgressMessage);
               // base.RaisePropertyChangedEvent("ProgressMessage");
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Clears the view model.
        /// </summary>
        internal void ClearViewModel()
        {
            Progress = 0;
            ProgressMax = 0;
            ProgressMessage = string.Format(m_ProgressMessageTemplate, 0);
            this.IsCancelled = false;
        }

        /// <summary>
        /// Advances the progress counter for the Progress dialog.
        /// </summary>
        /// <param name="incrementClicks">The number of 'clicks' to advance the counter.</param>
        internal void IncrementProgressCounter(int incrementClicks)
        {
            // Increment counter
            this.Progress += incrementClicks;

            // Update progress message
            var progress = Convert.ToSingle(p_Progress);
            var progressMax = Convert.ToSingle(p_ProgressMax);
            var f = (progress/progressMax)*100;
            var percentComplete = Single.IsNaN(f) ? 0 : Convert.ToInt32(f);
            this.ProgressMessage = string.Format(m_ProgressMessageTemplate, percentComplete);
        }

        /// <summary>
        /// Sets the progreess message to show that processing was cancelled.
        /// </summary>
        internal void ShowCancellationMessage()
        {
            this.ProgressMessage = m_CancellationMessage;
        }

        #endregion

        #region Property IsProgressShow
        private bool isShow;
        public bool IsShow
        {
            get { return isShow; }
            set
            {
                isShow = value;
                this.OnPropertyChanged("IsShow");
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this view model.
        /// </summary>
        /// <param name="mainWindowViewModel">The view model for this application's main window.</param>
        private void Initialize()
        {
            m_ProgressMessageTemplate = " {0}% 任务已完成";
            m_CancellationMessage = "任务取消";
            this.ClearViewModel();
        }

        #endregion
    }
}