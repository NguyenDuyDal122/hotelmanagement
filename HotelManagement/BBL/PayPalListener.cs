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
                // Gửi phản hồi HTML cho trình duyệt
                string responseString = @"
                    <!DOCTYPE html>
                    <html lang='vi'>
                    <head>
                        <meta charset='UTF-8'>
                        <title>Thanh toán thành công</title>
                        <style>
                            body {
                                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                background-color: #f2f4f8;
                                display: flex;
                                justify-content: center;
                                align-items: center;
                                height: 100vh;
                                margin: 0;
                            }
                            .container {
                                background-color: #ffffff;
                                padding: 40px;
                                border-radius: 12px;
                                box-shadow: 0 4px 12px rgba(0,0,0,0.1);
                                text-align: center;
                            }
                            .container h1 {
                                color: #2ecc71;
                                font-size: 32px;
                                margin-bottom: 10px;
                            }
                            .container p {
                                color: #333333;
                                font-size: 18px;
                            }
                            .checkmark {
                                font-size: 48px;
                                color: #2ecc71;
                                margin-bottom: 20px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='checkmark'>✔</div>
                            <h1>Thanh toán thành công!</h1>
                            <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi. Bạn có thể quay lại ứng dụng.</p>
                        </div>
                    </body>
                    </html>";

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
