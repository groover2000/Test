import {action, makeObservable, observable, computed} from 'mobx';
const URL_MAIN = 'https://api.github.com/search/repositories?q=';

//setlocalstorage -!

// const LEFT_PAGE = 'LEFT';
// const RIGHT_PAGE = 'RIGHT';

// const range = (from, to, step = 1) => {
//     let i = from;
//     const range = [];
  
//     while (i <= to) {
//       range.push(i);
//       i += step;
//     }
  
//     return range;
//   }


class SearchStore {
    inputValue = '';
    cardsStore = [];
    loadding = false;
    pageLimit = 10; // how many render
    totalCount = null; // total_count
    pageNeighbours = 0;
    currentPage = 1;

    totalPages = Math.ceil(this.totalCount / this.pageLimit)

    constructor() {
        makeObservable(this, {
            inputValue: observable,
            cardsStore: observable,
            loadding: observable,
            pageLimit: observable,
            totalCount: observable,
            setInput: action.bound,
            getCardsStore: action.bound,
            dragStart: action.bound,
            dragEnd: action.bound,
            dragLeave: action.bound,
            dragOver: action.bound,
            dropHandler: action.bound,
            sortCards: action
        });
    }

    setInput(value) {
        this.inputValue = value.trim();
    }

    async getCardsStore(subject) {
        this.loadding = true;
        const response = await fetch(`${URL_MAIN}${subject}`);
        const data = await response.json();
        this.cardsStore = data.items;
        this.totalCount = data.total_count
        this.loadding = false;
       
      
    }












    // dndrop-------------------------------
    dragStart(e, card) {
        console.log(`card ${card}`)
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
        this.cardsStore.map((c) => {
            if(c.id ===card.id) {
                return {...c, id: this.cardsStore.id}
            }
            if(c.id ===this.cardsStore.id) {
                return {...c, id: card.id}
            }
            return c;
        })
        e.target.style.background = 'white';
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
