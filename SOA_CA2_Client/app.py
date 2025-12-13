from flask import Flask, request, redirect, render_template, jsonify
import requests

app = Flask(__name__)

secret_key = "SOACA2APICLIENT"

API_Route = "http://localhost:5216/api"


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
            return redirect("/dashboard")
        else:
            print("Invalid login details")
            return render_template("login.html")

    return render_template("login.html")


@app.route("/dashboard")
def dashboard():
    return render_template("dashboard.html")


# to load in api data - https://www.geeksforgeeks.org/python/how-to-get-data-from-api-in-python-flask/
@app.route("/drinks")
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


if __name__ == "__main__":
    app.run(debug=True)
