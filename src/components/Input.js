import InputGroup from 'react-bootstrap/InputGroup'
import {FormControl} from 'react-bootstrap'
import {Button} from 'react-bootstrap';
import searchStore from '../store/searchStore';
import {observer} from 'mobx-react-lite';

const Input = observer(()=> {
  
    return (
        <div className = 'App' >
        <InputGroup className="mb-3">
       <FormControl onChange = {(e)=> searchStore.setInput(e.target.value)}
         placeholder="Начните вводить текст"
         aria-label=''
         aria-describedby="basic-addon2"
         value = {searchStore.inputValue}
        
       />
       <Button onClick ={()=>searchStore.getCardsStore(searchStore.inputValue)} variant="outline-secondary" id="button-addon2">
         Button
       </Button>
     </InputGroup>
       </div>
    )
}) 

export {Input}