using PayPalCheckoutSdk.Orders;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class PayPalListener
{
    public static async Task StartListeningAsync(Action<string, string> onPaymentSuccess)
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5000/paypal-success/");
        listener.Start();

        while (true)
        {
            var context = await listener.GetContextAsync();
            var request = context.Request;
            var response = context.Response;

            string token = request.QueryString["token"];
            string payerId = request.QueryString["PayerID"];

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(payerId))
            {
                // Xác nhận thanh toán từ PayPal
                var captureRequest = new OrdersCaptureRequest(token);
                captureRequest.RequestBody(new OrderActionRequest());
                var result = await PayPalPayment.CapturePayment(token);

                // Gửi phản hồi HTML cho trình duyệt
                string responseString = "<html><body><h2>Thanh toan thanh cong! Ban co the quay lai ung dung.</h2></body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                var output = response.OutputStream;
                await output.WriteAsync(buffer, 0, buffer.Length);
                output.Close();

                // Gọi callback để xử lý lưu dữ liệu
                onPaymentSuccess?.Invoke(token, payerId);
            }
            else
            {
                response.StatusCode = 400;
                response.Close();
            }
        }
    }
}
