using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NolowaFrontend.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        public Predicate<object> _CanExecute = p => { return true; }; // Always..
        public Action<object> _Execute = p => { }; // Do nothing..

        /// <summary>
        /// 커맨드 수행 조건이 변경될 때 발생할 이벤트
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 언제나 실행되며 아무것도 수행하지 않는 기본 커맨드 생성
        /// </summary>
        public RelayCommand() : this(null, null) { }

        /// <summary>
        /// 언제나 실행되는 커맨드 생성
        /// </summary>
        /// <param name="execute">실행시키려는 작업</param>
        public RelayCommand(Action<object> execute) : this(null, execute) { }

        /// <summary>
        /// 특정 조건에서 실행되는 커맨드 생성
        /// </summary>
        /// <param name="canExecute">커맨드 실행조건을 판단하는 Predicate</param>
        /// <param name="execute">실행시키려는 작업</param>
        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            if (canExecute != null)
                this._CanExecute = canExecute;

            if (execute != null)
                this._Execute = execute;
        }

        /// <summary>
        /// 커맨드가 수행될 수 있는지 조건을 확인
        /// </summary>
        /// <param name="parameter">커맨드 수행 조건 함수로 전달될 매개변수</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _CanExecute.Invoke(parameter);
        }

        /// <summary>
        /// 커맨드 수행
        /// </summary>
        /// <param name="parameter">커맨드로 전달될 매개변수</param>
        public void Execute(object parameter)
        {
            _Execute.Invoke(parameter);
        }

        /// <summary>
        /// 기존 동작을 지우지 않고, 그 보다 이전에 수행할 동작을 지정
        /// </summary>
        /// <param name="newAction">기존 동작보다 먼저 실행시킬 새 동작</param>
        public void AddNewActionBeforeExistingAction(Action<object> newAction)
        {
            var existingAciton = _Execute;
            _Execute = newAction;
            _Execute += existingAciton;
        }
    }
}
