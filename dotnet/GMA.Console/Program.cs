using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using XInput.Wrapper;

using NotificationsExtensions.Toasts;

namespace GMA.Console_
{
    class Program
    {
        static void Main(string[] args)
        {
            var thing = new NotifyMe();
            thing.Register();
            Console.WriteLine("Press enter to close");
            Console.ReadLine();
        }
    }
    public class NotifyMe
    {
        public void Register()
        {
            X.Gamepad gamepad = null;
            if (X.IsAvailable)
            {
                BindGamepad(X.Gamepad_1);
                BindGamepad(X.Gamepad_2);
                BindGamepad(X.Gamepad_3);
                BindGamepad(X.Gamepad_4);
            }
        }

        public void Update()
        {

        }
        public DateTime? LastNotified { get; set; } = null;
        public TimeSpan NotificationThreshold { get; set; } = TimeSpan.FromSeconds(10);
        public void SendNotification()
        {
            if(LastNotified != null && LastNotified.Value > (DateTime.Now - NotificationThreshold))
            {
                return;
            }
            LastNotified = DateTime.Now;
            PlaySound();
            Toast();
        }

        public void PlaySound()
        {
            /* Flush sound attribution:
             * Recorded by Mike Koenig
             * http://soundbible.com/2114-Flush-Toilet--2.html
             * */
            try
            {
                new SoundPlayer("flush.wav").Play();
            } catch { }
        }

        public void Toast()
        {
            /*
            var binding = new ToastBindingGeneric();
            var group = new AdaptiveGroup();
            var subGroup = new AdaptiveSubgroup();
            subGroup.Children.Add(new AdaptiveText
            {
                Text = "Knock, knock! Someone wants your attention!"
            });
            group.Children.Add(subGroup);
            binding.Children.Add(group);

            //var toast = new ToastNotification(toastContent.GetXml());
            */
        }

        public void BindGamepad(X.Gamepad gamepad)
        {
            if (gamepad != null)
            {
                gamepad.KeyDown += Gamepad_KeyDown;
                gamepad.StateChanged += Gamepad_StateChanged;
                X.StartPolling(gamepad);
            }
        }

        private void Gamepad_StateChanged(object sender, EventArgs e)
        {
            Trace.WriteLine("Gamepad state changed");
            //SendNotification();
        }

        private void Gamepad_KeyDown(object sender, EventArgs e)
        {
            Trace.WriteLine("Gamepad keydown");
            SendNotification();
        }
    }
}
