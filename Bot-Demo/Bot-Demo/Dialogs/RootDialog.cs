using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;

namespace Bot_Demo.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

//            if(activity.GetActivityType() == ActivityTypes.ConversationUpdate)
//            {
//                await context.Forward(new MenuDialog(), this.ResumeAfterMenuDialog, activity, CancellationToken.None);
//            }
//            else
//            {
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                await context.PostAsync($"You sent {activity.Text} which was {length} characters");

//            }
        }

        private async Task ResumeAfterMenuDialog(IDialogContext context, IAwaitable<string> result)
        {
            string menuDialogExitMessage = await result;
            await context.PostAsync(menuDialogExitMessage);
            context.Wait(this.MessageReceivedAsync);
        }
    }
}