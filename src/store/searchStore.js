import {action, makeObservable, observable, computed} from 'mobx';
const URL_MAIN = 'https://api.github.com/search/repositories?q=';

//setlocalstorage -!


class SearchStore {
    //search render state
    inputValue = '';
    cardsStore = [];
    loadding = false;
    secondInput = ''
    commentsStore = [];
     //search render state

    //pagination state
    totalRecords = null;
    pageLimit = 10;
    pageOffset = 0;
    currentPage = 0;
    //pagination state

    //pagination computed
    // totalPages = Math.ceil(this.totalRecords / this.pageLimit)
    // totalNumbers = (this.pageNeighbours * 2) + 3;
    // totalBlocks = this.totalNumbers + 2;
    //pagination computed

    currentCard = null;

 get totalPages() {
     return Math.ceil(this.totalRecords / this.pageLimit)
 }
 btnCommets(id, value) {
    console.log(this.cardsStore.comment)
    localStorage.setItem(id, JSON.stringify(this.cardsStore.comment))
    value = ''
   
 }

    constructor() {
        makeObservable(this, {
            inputValue: observable,
            cardsStore: observable,
            loadding: observable,
            totalRecords:observable,
            pageLimit: observable,
            pageOffset: observable,
            currentPage: observable,
            totalPages: computed,
            currentCard:observable,
            secondInput:observable,
            commentsStore:observable,
          
            btnCommets:action.bound,
            setInput: action.bound,
            getCardsStore: action.bound,
            dragStart: action.bound,
            dragEnd: action.bound,
            dragLeave: action.bound,
            dragOver: action.bound,
            dropHandler: action.bound,
            handlePageClick: action.bound,
            changeSel : action.bound, 
            sortCards: action.bound,
            setInput2: action.bound
            
        });
    }

    setInput(value) {
        this.inputValue = value.trim();
    }                                      // !!
    setInput2 (id, value){
        this.cardsStore.comment = value.trim();
       
    }

    async getCardsStore(subject) {
        this.loadding = true;
        const response = await fetch(`${URL_MAIN}${subject}`);
        const data = await response.json();
        this.cardsStore = Object.assign(data.items, data.comment = ''); //map
        this.totalRecords = this.cardsStore.length;
        this.loadding = false;  
        
    }

    handlePageClick(e) {

        this.currentPage = e.selected
        this.pageOffset = this.currentPage * this.pageLimit
        console.log (this.pageOffset)
    }
    changeSel(e){
        this.pageLimit = Number(e.target.value);
        // console.log(e.target.value)
    }



   
    






//e.path

    // dndrop-------------------------------
    dragStart(e, card) {
        this.currentCard = card.id
        console.log(`card ${this.currentCard}`)
    }

   dragLeave(e) {
    e.target.style.background = 'white';
   }

    dragEnd(e) {
      
    }

    dragOver(e) {
        e.preventDefault()
        e.target.style.background = 'black';
      
    }

    dropHandler(e, card) {
        e.preventDefault()
       e.target.style.background = "white"
            
    }

    sortCards(a, b) {
        if(a.id > b.id) {
            return 1
        }else {
            return -1;
        }
    }
   //=====================================================
    // react dnd lib!
}

export default new SearchStore()
