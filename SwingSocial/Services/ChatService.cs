using Java.Net;
using MLToolkit.Forms.SwipeCardView;
using SwingSocial.Sample.Helper;
using SwingSocial.Sample.Model;
using SwingSocial.Sample.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SwingSocial.Sample.Services
{
    internal class ChatService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        private static string BASE_URL = "http://expatcallers.com/";
        public List<ChatComment> ChatComments { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Emoji> Emojis { get; set; }
        public Chat Chat { get; set; }


        public ChatService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<ChatComment>> LoadChatComments() {
            ChatComments = new List<ChatComment>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetChatMain?LoggedId=" + SwipeCardView.UsrId+ "&ToProfileId="+ChatPage.ToProfileId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ChatComment> temp =
                        JsonSerializer.Deserialize<List<ChatComment>>(content, serializerOptions);
                    ChatComments = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ChatComments.AddExtraInfo();
        }
        //InsertChatConversation

        internal async void InsertConversation(string chatId,string profileIdTo,string conversation)
        {
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/InsertChatConversation?ChatId="+chatId+"&ProfileIdFrom="+SwipeCardView.UsrId.ToString()+"&ProfileIdTo="+profileIdTo+"&Conversation="+conversation, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<InsertResult> temp =
                        JsonSerializer.Deserialize<List<InsertResult>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        internal async Task<List<Emoji>> LoadEmojis()
        {
            Emojis = new List<Emoji>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetEmojis", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Emoji> temp =
                        JsonSerializer.Deserialize<List<Emoji>>(content, serializerOptions);
                    Emojis = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Emojis.AddExtraInfo();
        }

        internal async Task<List<Chat>> LoadChats()
        {
            Chats = new List<Chat>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetAllChats?Id=" + SwipeCardView.UsrId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Chat> temp =
                        JsonSerializer.Deserialize<List<Chat>>(content, serializerOptions);
                    Chats = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Chats;
        }

        internal async Task<List<ChatComment>> GetToProfileChatComments(string conversationText)
        {
            ChatComments = new List<ChatComment>();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/GetChatMain?LoggedId=" + SwipeCardView.UsrId + "&ToProfileId=" + ChatPage.ToProfileId, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<ChatComment> temp =
                        JsonSerializer.Deserialize<List<ChatComment>>(content, serializerOptions);
                    ChatComments = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            ChatComments = GetOnlyNewOnes(ChatComments, ChatPage.ChatViewModel.ChatComments, conversationText);

            return ChatComments.AddExtraInfo();
        }

        private List<ChatComment> GetOnlyNewOnes(List<ChatComment> commentsFromDB, ObservableCollection<ChatComment> commentsFromApp,string conversationText)
        {
            List<ChatComment> returnComments = new List<ChatComment>();
            bool alreadyInApp = false;
            foreach (ChatComment chatComment in commentsFromDB) 
            {
                alreadyInApp = false;
                foreach (var item in commentsFromApp)
                {
                    if (chatComment.ConversationId.Equals(item.ConversationId))
                    {
                        alreadyInApp = true;
                        break;
                    }
                }
                if (!alreadyInApp && chatComment.Conversation!= conversationText)
                {
                    returnComments.Add(chatComment);
                }

            }
            return returnComments;
        }

        internal async Task<Chat> ChatCreateUserChat(Guid id)
        {
            Chat = new Chat();
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/ChatCreateUserChat?ProfileIdFrom=" + SwipeCardView.UsrId + "&ProfileIdTo=" + id.ToString(), string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Chat temp =
                        JsonSerializer.Deserialize<Chat>(content, serializerOptions);
                    Chat = temp;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Chat;
        }

        internal async Task<string> DeleteChat(Chat chat)
        {
            string response = string.Empty;
            Uri uri = new Uri(string.Format($"http://swingsocial.club:5001/api/User/ChatDeleteChat/{chat.ChatId}", string.Empty));
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }
    }

}
