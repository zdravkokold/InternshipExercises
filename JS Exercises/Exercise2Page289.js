class Book {
    constructor(_title, _author, _price) {
        this.title = _title;
        this.author = _author;
        this.price = _price;
    }

    set title(newTitle) {
        if (newTitle.length < 3) {
            throw new Error('Title not valid!');
        }
        this._title = newTitle;
    }

    set author(newAuthor) {
        if (this.startsWithDigit(newAuthor)) {
            throw new Error('Author not valid!');
        }
        this._author = newAuthor;
    }

    set price(newPrice) {
        if (newPrice <= 0) {
            throw new Error('Price not valid!');
        }
        this._price = parseFloat(newPrice).toFixed(1);
    }

    get title() {
        return this._title;
    }

    get author() {
        return this._author;
    }

    get price() {
        return parseFloat(this._price.toFixed(1));
    }

    startsWithDigit(inputString) {
        return /^\d/.test(inputString);
    }

    displayBook() {
        console.log(`${this._title} - ${this._author} - ${this._price}`);
    }
}

try {
    let book = new Book("Harry Potter", "J. K. Rowling", 20);
    book.displayBook();
} catch (error) {
    console.error(`Error: ${error.message}`);
}