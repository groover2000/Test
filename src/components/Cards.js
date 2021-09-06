import searchStore from '../store/searchStore';
import {observer} from 'mobx-react-lite';
import Card from 'react-bootstrap/Card';
import {Row} from 'react-bootstrap';
import {Col} from 'react-bootstrap';
import {Image} from 'react-bootstrap';
import {FormControl} from 'react-bootstrap'
import {Button} from 'react-bootstrap';
import InputGroup from 'react-bootstrap/InputGroup'

// stargazers_count
// watchers_count

const Cards = observer(() => {

   
// /.slice().sort(searchStore.sortCards)

    let users = searchStore.cardsStore.map((us)=> {
        return( 
         <Col  key = {us.id}
         draggable = {true}
         onDragStart = {(e) => searchStore.dragStart(e, us)}  //if else target
         onDragLeave = {(e) =>searchStore.dragLeave(e)}
         onDragEnd = {(e) => searchStore.dragEnd(e)}
         onDragOver = {(e) => searchStore.dragOver(e)}
         onDrop = {(e) => searchStore.dropHandler(e, us)}
         >
            <Card  style={{ width: '350px' }}>
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
      
     </Col>
    )
    })
    
    if(searchStore.loadding){
    return <div className="App">
        Loading...
    </div>
}
return <Row xs = {1} md ={3} className="App ">
    {users}
</Row>
})

export {Cards}