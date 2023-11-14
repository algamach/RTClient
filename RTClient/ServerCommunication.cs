using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class ServerCommunication
{
    private TcpClient client;

    public ServerCommunication()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 12345);
        }
        catch
        {
            MessageBox.Show("Перезагрузите сервер и клиент!", "Нет соединения с сервером", MessageBoxButton.OK, MessageBoxImage.Error);
            System.Environment.Exit(0);
        }
    }

    public async Task<string> SendMessageAndGetResponse(string message)
    {
        try
        {
            // Получение потока данных для взаимодействия с сервером
            NetworkStream stream = client.GetStream();

            // Преобразование сообщения в массив байтов и отправка
            byte[] data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);

            // Инициализация массива, чтение и преобразование байтов в строку
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
