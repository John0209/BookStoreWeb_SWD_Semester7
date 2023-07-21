function NewBook(newUrl, newName, newQuantity, newPrice, newType, newNote, newAmount) {
    this.newUrl = newUrl;
    this.newName = newName;
    this.newQuantity = newQuantity;
    this.newPrice = newPrice;
    this.newType = newType;
    this.newNote = newNote;
    this.newAmount = newAmount;
}

function OldBook(id, oldQuantity, oldNote) {
    this.id = id;
    this.oldQuantity = oldQuantity;
    this.oldNote = oldNote;
}