# Shopping-Website-Demo
> Use the Microsoft ASP.NET MVC framework to create a shopping website, the main purpose is to learn the back-end framework independently, learn basic operations and extended functions from practice, including basic knowledge of CRUD, member management, database management, message boards, etc.

###### The home page is a demonstration of the Read function, and the product information in the database is displayed one by one
![image](https://user-images.githubusercontent.com/101872264/161693598-6fa3f464-aeef-4cc2-a5fa-dab8574c0f93.png)
**The home page section uses HTML syntax for simple sorting of data.**
```
<div class="row">
    @foreach (var product in this.Model)
    {
        <div class="col-sm-6 col-md-4">
            <div class="thumbnail">
                <img class="ProductImg"src="@product.DefaultImageURL"alt="商品圖片"/>
                <div class="caption">
                    <h3>@product.Name</h3>
                    <p>@product.Description</p>
                    <p>售價：@product.Price</p>
                    <p>庫存：@product.Quantity</p>
                </div>
            </div>
        </div>
    }
</div>
```
**A method using `@Url.Action` causes the specified action**
```
<a href="@Url.Action("Details", new { id = product.Id })" class="btn btn-default" role="button">詳細資訊</a>
```

###### The following figure demonstrates the creation function, you can add product information, the basic information includes product name, description, category, price and quantity, etc.
![image](https://user-images.githubusercontent.com/101872264/161694521-9cd6f1f7-f28d-4121-8d61-02c58a03e4f2.png)
**Use `SaveChanges()` to store the returned data `postback` to the database**
```
using (Models.AzureDBEntities db = new Models.AzureDBEntities())
                {

                    // 將回傳資料postback加入至Products
                    db.Products.Add(postback);
                    // 儲存異動資料
                    db.SaveChanges();
                    // 設定成功訊息
                    TempData["ResultMessage"] = String.Format("商品[{0}]成功建立", postback.Name);
                    //轉導Product/Index頁面
                    return RedirectToAction("Index");
                }
```
###### It can be seen on the product management page, and it also includes the functions of Read, Update and Delete, allowing managers to perform related operations here.
![image](https://user-images.githubusercontent.com/101872264/161695343-34831cd4-523d-43cf-80ce-c5f52894edf7.png)![image](https://user-images.githubusercontent.com/101872264/161696917-92b1fdad-a3e3-4e98-bda6-28a0b5e6c776.png)
###### There is not much difference from the above operation, mainly the content of `View`, use `@Html.LabelFor` and other methods to create edit blocks and return information**
```
<div class="form-group">
            @Html.LabelFor(model => model.Name, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Name, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-danger" })
            </div>
        </div>
```

###### In the web.config data, the default connection string is DefaultConnection, so we use the internal AspNetUsers data table for member-related operations and management; and add a nickname field to log in by nickname
![image](https://user-images.githubusercontent.com/101872264/161697025-29e7c030-b54f-4bf9-bca8-304c3e718d77.png)
###### Can also be modified for personnel information
![image](https://user-images.githubusercontent.com/101872264/161699264-7cb7dc90-81f5-4cd5-adbb-9fa4ea4a3d45.png)

###### The shopping cart function is accomplished through Ajax, including clicking on the product on the page to add to the shopping cart, or deleting the product from the shopping cart, emptying the product, when the staff confirms, click the checkout button in the shopping cart Take to checkout page
![image](https://user-images.githubusercontent.com/101872264/161699514-d4d934f2-f299-4ed4-bca6-c06d99a70653.png)
**Obtain the data selected by the current client through the Session mechanism, and keep the current state until the client cancels or removes**
```
        if (System.Web.HttpContext.Current.Session["Cart"] == null) 
        {
            var order = new Cart();
            System.Web.HttpContext.Current.Session["Cart"] = order;
            //return (Cart)System.Web.HttpContext.Current.Session["Cart"];
        }
        return (Cart)System.Web.HttpContext.Current.Session["Cart"];
```
```
public ActionResult AddToCart(int id)
        {
            var currentCart = Models.Operation.GetCurrentCart();
            currentCart.AddProduct(id);
            return PartialView("_CartPartial");
        }
```
**Use Ajax syntax to add the product with the corresponding ID to the shopping cart**
```
function AddToCart(productId) {
            $.ajax({
                type: 'POST',
                url: '@Url.Action("AddToCart","Cart")',
                data: { id: productId }
            })
                .done(function (msg) {
                    $('li#Cart').html(msg);
                });
        }
```
>**The next part is to add some variability and variety from the base operations. In fact, there are more interesting functions, you can try to understand more**
###### Directed to the checkout page via the checkout button in the shopping cart
![image](https://user-images.githubusercontent.com/101872264/161701074-ee8630a6-1d29-47ff-8df0-99a4bd3b7003.png)
###### Order information can be viewed on the order management page, and relevant order information can be searched by the name of the purchasing member
![image](https://user-images.githubusercontent.com/101872264/161701135-ac9447fe-ed98-4baa-9649-63fb8f2814df.png)
###### You can also view the order details
![image](https://user-images.githubusercontent.com/101872264/161701201-d5e1042a-9576-418d-ad26-d81133571b43.png)
###### In addition, each product displayed on the homepage has detailed information to view, and adds a message board function
![image](https://user-images.githubusercontent.com/101872264/161702478-4f7c22cc-b1f5-449a-936f-42228ecc15be.png)

###### This project serves as a record of the self-study back-end, and learned the relevant basic knowledge during the learning process, including the implementation of CRUD, members and data management
###### The next step will be to release the network, through Azure, AWS, etc. Of course, there are more skills to learn, I hope I can learn more
