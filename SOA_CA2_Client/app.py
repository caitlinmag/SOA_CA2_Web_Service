from wsgiref import headers
from flask import Flask, request, redirect, render_template, jsonify, url_for, session
import requests

app = Flask(__name__)

secret_key = "SOACA2APICLIENT"
API_Route = (
    "https://soaapiservice-h4h9a3fefqhyc0ds.francecentral-01.azurewebsites.net/api"
)
# API_Route = "http://localhost:5216/api"
app.secret_key = secret_key


# using the tutorial: https://www.restapiexample.com/python/consuming-a-restful-api-with-python-and-flask/?utm_source=chatgpt.com
@app.route("/", methods=["GET", "POST"])
def login():
    if request.method == "POST":
        username = request.form["username"]
        password = request.form["password"]

        response = requests.post(
            f"{API_Route}/User/Authenticate",
            json={"username": username, "password": password},
        )

        # https://fastapi.tiangolo.com/tutorial/response-status-code/
        if response.status_code == 200:  # login is successful
            try:
                authorise_data = response.json()
                jwt_token = authorise_data.get("token")

                if jwt_token:  # checking if user has been given a token
                    session["jwt_token"] = jwt_token
                    print("Token has been found")
                    return redirect("/dashboard")
                else:
                    print("Token not found")
                    return render_template("login.html")
            except Exception:
                return render_template("login.html")
        else:
            print("Invalid login details")
            return render_template("login.html")
    return render_template("login.html")


@app.route("/dashboard")
def dashboard():
    return render_template("dashboard.html")


# DRINKS FUNCTIONALITY
# to load in api data - https://www.geeksforgeeks.org/python/how-to-get-data-from-api-in-python-flask/
@app.route("/drinks", methods=["GET", "POST"])
def drinks():
    drinks_route = f"{API_Route}/DrinkItems"

    try:
        response = requests.get(drinks_route)
        response.raise_for_status()
        data = response.json()
        return render_template("drinks.html", drinks_list=data)

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        return render_template("drinks.html", drinks_list=[])


@app.route("/add_drink", methods=["POST"])
def add_drink():
    # fix for jwt token from - https://stackoverflow.com/questions/20620300/http-content-type-header-and-json
    # headers required below
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    # create drink, similar to the login input
    if request.method == "POST":
        name = request.form["name"]
        drinktype = request.form["drink_type"]
        price = request.form["price"]
        extras = request.form["extras"]
        supplierid = request.form["supplier_id"]

        try:
            response = requests.post(
                f"{API_Route}/DrinkItems",
                json={
                    "drinkName": name,
                    "drinkType": drinktype,
                    "price": price,
                    "extras": extras,
                    "supplierId": supplierid,
                },
                headers=headers,
            )
            response.raise_for_status()
        except requests.exceptions.HTTPError as err:
            error = {"error": f"Error occured: {err}"}
            print("Error message:", error)
            pass
        return redirect(url_for("drinks"))


@app.route("/update_drink", methods=["POST"])
def update_drink():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    id = request.form["drink_id"]

    try:
        response = requests.put(
            f"{API_Route}/DrinkItems/{id}",
            json={
                "drinkName": request.form["name"],
                "drinkType": request.form["drink_type"],
                "price": request.form["price"],
                "extras": request.form["extras"],
                "supplierId": request.form["supplier_id"],
            },
            headers=headers,
        )

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        pass

    print("error:", response.text)
    return redirect(url_for("drinks"))


@app.route("/delete_drink", methods=["POST"])
def delete_drink():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    id = request.form["drink_id"]

    try:
        response = requests.delete(
            f"{API_Route}/DrinkItems/{id}",
            headers=headers,
        )

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        pass

    print("error:", response.text)
    return redirect(url_for("drinks"))


# SALES FUNCTIONALITY
@app.route("/sales")
def sales():
    sales_route = f"{API_Route}/DrinkSales"

    try:
        response = requests.get(sales_route)
        response.raise_for_status()
        data = response.json()
        return render_template("sales.html", sales_list=data)

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        return render_template("sales.html", sales_list=[])


@app.route("/add_sale", methods=["POST"])
def add_sale():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    if request.method == "POST":
        drink_id = request.form["drink_id"]
        quantity = request.form["quantity"]

        try:
            response = requests.post(
                f"{API_Route}/DrinkSales",
                json={
                    "drinkItemId": drink_id,
                    "quantity": quantity,
                },
                headers=headers,
            )
            response.raise_for_status()
        except requests.exceptions.HTTPError as err:
            error = {"error": f"Error occured: {err}"}
            print("Error message:", error)
            pass
        return redirect(url_for("sales"))


@app.route("/update_sale", methods=["POST"])
def update_sale():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    id = request.form["sale_id"]

    try:
        response = requests.put(
            f"{API_Route}/DrinkSales/{id}",
            json={
                "drinkItemId": request.form["drink_id"],
                "quantity": request.form["quantity"],
            },
            headers=headers,
        )

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        pass

    print("error:", response.text)
    return redirect(url_for("sales"))


@app.route("/delete_sale", methods=["POST"])
def delete_sale():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    id = request.form["sale_id"]

    try:
        response = requests.delete(
            f"{API_Route}/DrinkSales/{id}",
            headers=headers,
        )

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        pass

    print("error:", response.text)
    return redirect(url_for("sales"))


# SUPPLIERS FUNCTIONALITY
@app.route("/suppliers")
def suppliers():
    suppliers_route = f"{API_Route}/Suppliers"

    try:
        response = requests.get(suppliers_route)
        response.raise_for_status()
        data = response.json()
        return render_template("suppliers.html", suppliers_list=data)

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        return render_template("suppliers.html", suppliers_list=[])


@app.route("/add_supplier", methods=["POST"])
def add_supplier():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    if request.method == "POST":
        supplier_name = request.form["name"]
        location = request.form["location"]
        stock_level = request.form["stock"]

        try:
            response = requests.post(
                f"{API_Route}/Suppliers",
                json={
                    "supplierName": supplier_name,
                    "location": location,
                    "stockLevel": stock_level,
                },
                headers=headers,
            )
            response.raise_for_status()
        except requests.exceptions.HTTPError as err:
            error = {"error": f"Error occured: {err}"}
            print("Error message:", error)
            pass
        return redirect(url_for("suppliers"))


@app.route("/update_supplier", methods=["POST"])
def update_supplier():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    id = request.form["id"]

    try:
        response = requests.put(
            f"{API_Route}/Suppliers/{id}",
            json={
                "supplierName": request.form["name"],
                "location": request.form["location"],
                "stockLevel": request.form["stock"],
            },
            headers=headers,
        )

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        pass

    print("error:", response.text)
    return redirect(url_for("suppliers"))


@app.route("/delete_supplier", methods=["POST"])
def delete_supplier():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    id = request.form["supplier_id"]

    try:
        response = requests.delete(
            f"{API_Route}/Suppliers/{id}",
            headers=headers,
        )

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        pass

    print("error:", response.text)
    return redirect(url_for("suppliers"))


# USERS FUNCTIONALITY
@app.route("/users")
def users():
    users_route = f"{API_Route}/User"

    try:
        response = requests.get(users_route)
        response.raise_for_status()
        data = response.json()
        return render_template("users.html", users_list=data)

    except requests.exceptions.HTTPError as err:
        error = {"error": f"Error occured: {err}"}
        print("Error message:", error)
        return render_template("users.html", users_list=[])


@app.route("/add_user", methods=["POST"])
def add_user():
    token = session.get("jwt_token")
    headers = {"Content-Type": "application/json"}

    if token:
        headers["Authorization"] = f"Bearer {token}"
    else:
        print("not allowed")
        pass

    if request.method == "POST":
        id = request.form["id"]
        username = request.form["username"]
        password = request.form["password"]

        try:
            response = requests.post(
                f"{API_Route}/User",
                json={
                    "id": id,
                    "userName": username,
                    "password": password,
                },
                headers=headers,
            )
            response.raise_for_status()
        except requests.exceptions.HTTPError as err:
            error = {"error": f"Error occured: {err}"}
            print("Error message:", error)
            pass
        return redirect(url_for("users"))


if __name__ == "__main__":
    app.run(debug=True)
