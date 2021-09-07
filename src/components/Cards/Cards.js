import searchStore from '../../store/searchStore';
import {observer} from 'mobx-react-lite';
import './cardd.css'


// stargazers_count
// watchers_count

const Cards = observer(() => {

   
// /.slice().sort(searchStore.sortCards)

    let users = searchStore.cardsStore.slice(searchStore.pageOffset, searchStore.pageOffset + searchStore.pageLimit ).map((us,i)=> {
        return( 
        <div key = {us.id} id = {us.id} className='cards'
        draggable = {true}
        onDragStart = {(e) => searchStore.dragStart(e, us)}  
        onDragLeave = {(e) =>searchStore.dragLeave(e)}
        onDragEnd = {(e) => searchStore.dragEnd(e)}
        onDragOver = {(e) => searchStore.dragOver(e)}
        onDrop = {(e) => searchStore.dropHandler(e, us)}
        >
            <a className = 'cards-link' href = {us.html_url}></a>
            <h2 className="cards-title">{us.name}</h2>
            <div className="cards-body">
                 <img className = 'cards-img' src={us.owner.avatar_url} alt="" />
                <p className="name">{us.owner.login}</p>
            </div>
            <div className="cards-counter">
                <p className="cards-star">{us.stargazers_count}</p>
                <p className="cards-star">{us.watchers_count}</p>
            </div>
            <form action="" className="cards-form">
                <input type="text" name = {us.id} className="cars-form__input" placeholder ='Комментарий'
                 value = {searchStore.comment} onChange = {(e)=> searchStore.setInput2(us.id,e.target.value)} />
                <input type="button" className="cars-form__btn" onClick={()=>searchStore.btnCommets(us.id, us.comment)} />
            </form>
            
        </div>
        
    )
    })
    
    if(searchStore.loadding){
    return <div className="App">
        Loading...
    </div>
}
return <div className="App">
    {users}
    
</div>
    

})

export {Cards}





{/* <Col  
>
   <Card  style={{ width: '350px' }}
   key = {us.id}
   id ={us.id}
   draggable = {true}
   onDragStart = {(e) => searchStore.dragStart(e, us)}  //if else target
   onDragLeave = {(e) =>searchStore.dragLeave(e)}
   onDragEnd = {(e) => searchStore.dragEnd(e)}
   onDragOver = {(e) => searchStore.dragOver(e)}
   onDrop = {(e) => searchStore.dropHandler(e, us)}>
   <Card.Body>
     <Card.Title>{us.name}</Card.Title>
     <Card.Subtitle className="mb-2 text-muted">Author: {us.owner.login}</Card.Subtitle>
     <Card.Img src = {us.owner.avatar_url} />
     <Card.Text>
       Stars: {us.stargazers_count}
     </Card.Text>
     <Card.Link href={us.html_url}>Git Link</Card.Link>
     <InputGroup className="mb-3">
       <FormControl
       placeholder="Ваш комментарий"
       aria-label="Recipient's username"
       aria-describedby="basic-addon2"
       />
       <Button variant="outline-secondary" id="button-addon2">
       Button
       </Button>
   </InputGroup>
   </Card.Body>
 </Card>

</Col> */}