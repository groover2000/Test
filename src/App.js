import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import {Input} from './components/Input'
import {Cards} from './components/Cards/Cards'
import {PageCounter} from './components/pageCounter/pageCounter'

function App() {
  return (
    <>
   <Input/>
   <Cards/>
   <PageCounter/>
   </>
  );
}

export default App;
