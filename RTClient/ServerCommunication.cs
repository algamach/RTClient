using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class ServerCommunication
{
    private TcpClient client;

    public ServerCommunication()
    {
        client = new TcpClient("127.0.0.1", 12345);
    }

    public async Task<string> SendMessageAndGetResponse(string message)
    {
        try
        {
            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);

            byte[] responseBytes = new byte[1024];
            int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
            return Encoding.UTF8.GetString(responseBytes, 0, bytesRead);
        }
        catch (Exception ex)
        {
            return "Ошибка: " + ex.Message;
        }
    
    }

    public void CloseConnection()
    {
        client.Close();
    }
}
