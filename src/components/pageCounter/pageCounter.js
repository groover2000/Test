import React from "react";
import searchStore from '../../store/searchStore.js';
import ReactPaginate from 'react-paginate'
import {observer} from 'mobx-react-lite';
import '../Cards/cardd.css'
import './pagination.css'

const PageCounter = observer(()=> {
  
   
    if (!searchStore.totalRecords || searchStore.totalPages === 1) return (
       <div className="App">
    <form action="">
     <select onClick = {searchStore.changeSel} name="" id="">
       <option value="10">10</option>
       <option value="20">20</option>
       <option value="30">30</option>
     </select>
    </form>
    </div>);

    return (
        <div className="App">
        <form action="">
         <select onClick = {searchStore.changeSel} name="" id="">
           <option value="10">10</option>
           <option value="20">20</option>
           <option value="30">30</option>
         </select>
        </form>
    <ReactPaginate
          previousLabel={'<'}
          nextLabel={'>'}
          breakLabel={'...'}
          breakClassName={'break-me'}
          pageCount={searchStore.totalPages}
          marginPagesDisplayed={2}
          pageRangeDisplayed={3}
          onPageChange={searchStore.handlePageClick}
          containerClassName={'pagination'}
          activeClassName={'active'}
        />
        </div>
      );

})

export {PageCounter}