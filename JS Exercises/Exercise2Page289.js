class Book {
    constructor(title, author, price) {
        this._title = title;
        this._author = author;
        this._price = price;
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
        return parseFloat(this._price);
    }

    startsWithDigit(inputString) {
        return /^\d/.test(inputString);
    }

    displayBook() {
        console.log(`${this.title} - ${this.author} - ${this.price}`);
    }
}

class GoldenBookEdition extends Book {
    constructor(title, author, price) {
        super(title, author, price);
    }

    get price() {
        const basePrice = super.price;
        const goldenPrice = basePrice * 1.3;
        return parseFloat(goldenPrice.toFixed(1));
    }
}

try {
    let goldenBook = new GoldenBookEdition("Harry Potter", "J. K. Rowling", 50);
    console.log("\nGolden Edition Book:");
    goldenBook.displayBook();
} catch (error) {
    console.error(`Error: ${error.message}`);
}