import os

from cs50 import SQL
from flask import Flask, flash, redirect, render_template, request, session
from flask_session import Session
from tempfile import mkdtemp
from werkzeug.exceptions import default_exceptions
from werkzeug.security import check_password_hash, generate_password_hash

from helpers import apology, login_required, lookup, usd

# Configure application
app = Flask(__name__)

# Ensure templates are auto-reloaded
app.config["TEMPLATES_AUTO_RELOAD"] = True

# Ensure responses aren't cached
@app.after_request
def after_request(response):
    response.headers["Cache-Control"] = "no-cache, no-store, must-revalidate"
    response.headers["Expires"] = 0
    response.headers["Pragma"] = "no-cache"
    return response

# Custom filter
app.jinja_env.filters["usd"] = usd

# Configure session to use filesystem (instead of signed cookies)
app.config["SESSION_FILE_DIR"] = mkdtemp()
app.config["SESSION_PERMANENT"] = False
app.config["SESSION_TYPE"] = "filesystem"
Session(app)

# Configure CS50 Library to use SQLite database
db = SQL("sqlite:///finance.db")


@app.route("/")
@login_required
def index():
    """Show portfolio of stocks"""
    return apology("TODO")


@app.route("/buy", methods=["GET", "POST"])
@login_required
def buy():
    """Buy shares of stock"""

    stock = lookup(request.form.get("symbol"))
    if request.method == "GET":
        print("test")
        return render_template("buy.html")

    else:
        

        shares = lookup(request.form.get("shares"))
        customerCash = db.execute("SELECT cash FROM users WHERE id = 1")

        if not stock:
            return apology("Invalid Stock Symbol")
        else:

            if stock.price < customerCash:

                db.execute("UPDATE users SET cash = cash - (:stockCost * :amount) WHERE id = 1", stockCost = stock.price, amount = shares)
                db.execute("INSERT INTO history VALUES (:customer, :symbol, :price, :time)", customer = db.execute("SELECT username FROM users WHERE id = 1"), symbol = stock.symbol, price = stock.price, time = db.execute("CURRENT_TIMESTAMP"))
                return redirect("/")
            else:
                return apology("Not Enough Funds")
    return apology("TODO")

@app.route("/history")
@login_required
def history():
    """Show history of transactions"""
    return apology("TODO")


@app.route("/login", methods=["GET", "POST"])
def login():
    """Log user in"""

    # Forget any user_id
    session.clear()

    # User reached route via POST (as by submitting a form via POST)
    if request.method == "POST":

        # Ensure username was submitted
        if not request.form.get("username"):
            return apology("must provide username", 403)

        # Ensure password was submitted
        elif not request.form.get("password"):
            return apology("must provide password", 403)

        # Query database for username
        rows = db.execute("SELECT * FROM users WHERE username = :username",
                          username=request.form.get("username"))

        # Ensure username exists and password is correct
        if len(rows) != 1 or not check_password_hash(rows[0]["hash"], request.form.get("password")):
            return apology("invalid username and/or password", 403)

        # Remember which user has logged in
        session["user_id"] = rows[0]["id"]

        # Redirect user to home page
        return redirect("/")

    # User reached route via GET (as by clicking a link or via redirect)
    else:
        return render_template("login.html")


@app.route("/logout")
def logout():
    """Log user out"""

    # Forget any user_id
    session.clear()

    # Redirect user to login form
    return redirect("/")


@app.route("/quote", methods=["GET", "POST"])
@login_required
def quote():
    """Get stock quote."""

    if request.method == "GET":
        return render_template("quote.html")
    else:
        quote = lookup(request.form.get("symbol"))
        if not quote:
            return apology("Invalid Stock Symbol")
        else:
            return render_template("quoted.html", stock = quote)


@app.route("/register", methods=["GET", "POST"])
def register():
    """Register user"""

    if request.method == "POST":
        username = request.form.get("username")
        password = request.form.get("password")
        conf = request.form.get("confirmation")

        if not username:
            return apology("Missing Username!")
        elif not password:
            return apology("Missing Password!")
        elif not conf:
            return apology("Missing Password Confirmation!")
        elif conf != password:
            return apology("Password and Confirmation do not match!")
        else:
            db.execute("INSERT INTO users (username, hash) VALUES(:name, :password)", name = request.form.get("username"), password = generate_password_hash(request.form.get("password")))
            return redirect('/')

    else :
        return render_template('register.html')


@app.route("/sell", methods=["GET", "POST"])
@login_required
def sell():
    """Sell shares of stock"""
    return apology("TODO")


def errorhandler(e):
    """Handle error"""
    return apology(e.name, e.code)


# listen for errors
for code in default_exceptions:
    app.errorhandler(code)(errorhandler)
