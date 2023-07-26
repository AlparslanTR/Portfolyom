using NToastNotify;

namespace IdeBlogMvc.CustomValidations
{
    public class ToastrMessages : IToastNotification
    {
        private readonly IToastNotification _toastNotification;

        public ToastrMessages(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }

        public void AddAlertToastMessage(string message = null, LibraryOptions toastOptions = null)
        {
             _toastNotification.AddAlertToastMessage(message, toastOptions);
        }

        public void AddErrorToastMessage(string message = null, LibraryOptions toastOptions = null)
        {
            _toastNotification.AddErrorToastMessage(message, toastOptions);
        }

        public void AddInfoToastMessage(string message = null, LibraryOptions toastOptions = null)
        {
            _toastNotification.AddInfoToastMessage(message, toastOptions);  
        }

        public void AddSuccessToastMessage(string message = null, LibraryOptions toastOptions = null)
        {
            _toastNotification.AddSuccessToastMessage(message, toastOptions);
        }

        public void AddWarningToastMessage(string message = null, LibraryOptions toastOptions = null)
        {
            _toastNotification.AddWarningToastMessage(message, toastOptions);
        }

        public IEnumerable<IToastMessage> GetToastMessages()
        {
            return _toastNotification.GetToastMessages();
        }

        public IEnumerable<IToastMessage> ReadAllMessages()
        {
            return _toastNotification.ReadAllMessages();
        }

        public void RemoveAll()
        {
            _toastNotification.RemoveAll();
        }
    }
}
