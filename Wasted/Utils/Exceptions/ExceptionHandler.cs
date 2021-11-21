using System;
using Xamarin.Forms;

namespace Wasted.Utils.Exceptions
{
    public static class ExceptionHandler
    {
        public static void WrapFunctionCall(Action function, ContentPage page)
        {
            try
            {
                function();
            }
            catch (ArgumentNullException)
            {
                page.DisplayAlert("", "Please fill all fields", "OK");
            }
            catch (UserAlreadyExistsException)
            {
                page.DisplayAlert("", "User with this username already exists", "OK");
            }
        }
    }
}