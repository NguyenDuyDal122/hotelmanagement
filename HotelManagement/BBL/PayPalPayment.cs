using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PayPalPayment
{
    private static string clientId = "AUDP0T95Gl86V2yjnLyXtdtVzL0_qyv0woP2_In8s9K2NFp6CF_fDg7MExg1f-2u2G67W5t2416pUZpi";     // 👉 Thay bằng Client ID của bạn
    private static string secret = "EPHSn_VPt1y7-aXiitKaZhn4fbpraGe2lnuD4SVjySQKD2LGng1hVU_KFEfeXq0wHgXMw3eNLdrq6WE9";          // 👉 Thay bằng Secret của bạn

    private static PayPalEnvironment environment = new SandboxEnvironment(clientId, secret);
    private static HttpClient client = new PayPalHttpClient(environment);

    public static async Task<string> CreatePayment(decimal amount)
    {
        var orderRequest = new OrderRequest()
        {
            CheckoutPaymentIntent = "CAPTURE",
            PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = "USD",
                        Value = amount.ToString("F2")
                    }
                }
            },
            ApplicationContext = new ApplicationContext
            {
                ReturnUrl = "http://localhost:5000/paypal-success",   // 👉 URL sau khi thanh toán thành công
                CancelUrl = "https://example.com/cancel"     // 👉 URL nếu huỷ
            }
        };

        var request = new OrdersCreateRequest();
        request.Prefer("return=representation");
        request.RequestBody(orderRequest);

        var response = await client.Execute(request);
        var result = response.Result<Order>();

        foreach (var link in result.Links)
        {
            if (link.Rel.Equals("approve"))
            {
                return link.Href;  // 👉 Đây là URL PayPal để redirect người dùng đến
            }
        }

        return null;
    }
    public static async Task<Order> CapturePayment(string token)
    {
        var captureRequest = new OrdersCaptureRequest(token);
        captureRequest.RequestBody(new OrderActionRequest());

        var response = await client.Execute(captureRequest);
        return response.Result<Order>();
    }
}
